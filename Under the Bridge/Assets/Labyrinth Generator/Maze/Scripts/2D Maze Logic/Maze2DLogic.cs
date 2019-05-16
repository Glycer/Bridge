using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Maze2DLogic
{
    #region Surveying
    public static Dictionary<int, int[]> directions = new Dictionary<int, int[]> {
            { 0, new int[] {1,0,0} },
            { 1, new int[] {0,1,0} },
            { 2, new int[] {-1,0,0} },
            { 3, new int[] {0,-1,0} }
        };

    static Dictionary<int, int> adjDirectionFlips = new Dictionary<int, int> { { 0, 2 }, { 1, 3 }, { 2, 0 }, { 3, 1 } };

    public static void Survey(int rowIndex, int colIndex, Maze2DSquare[,] grid)
    {
        Maze2DSquare square = grid[rowIndex, colIndex];

        #region Cardinal Directions
        Maze2DSquare north = (rowIndex < grid.GetLength(0) - 1) ? grid[rowIndex + 1, colIndex] : null;
        Maze2DSquare east = (colIndex < grid.GetLength(1) - 1) ? grid[rowIndex, colIndex + 1] : null;
        Maze2DSquare south = (rowIndex != 0) ? grid[rowIndex - 1, colIndex] : null;
        Maze2DSquare west = (colIndex != 0) ? grid[rowIndex, colIndex - 1] : null;
        #endregion

        Maze2DSquare[] adjacentSquares = new Maze2DSquare[] { north, east, south, west };
        square.adjacents = adjacentSquares;
    }

    public static void SurveyOutlet(Maze2DSquare rootSquare, int adjacencyIndex, Maze2DSquare[,] grid, out Maze2DSquare outletSquare)
    {
        rootSquare.adjacents[adjacencyIndex] = new Maze2DSquare(
            rootSquare.xCoord + directions[adjacencyIndex][0],
            rootSquare.yCoord + directions[adjacencyIndex][1]
            );

        outletSquare = rootSquare.adjacents[adjacencyIndex];
        outletSquare.isOutlet = true;

        if (outletSquare.xCoord >= 0 && outletSquare.xCoord < grid.GetLength(0) &&
            outletSquare.yCoord >= 0 && outletSquare.yCoord < grid.GetLength(1))
        {

            grid[outletSquare.xCoord, outletSquare.yCoord] = outletSquare;
            Survey(outletSquare.xCoord, outletSquare.yCoord, grid);

            //Fill open slot of any square adjacent to this new outlet
            for (int i = 0; i < outletSquare.adjacents.Length; i++)
                if (outletSquare.adjacents[i] != null)
                    outletSquare.adjacents[i].adjacents[adjDirectionFlips[i]] = outletSquare;
        }
    }
    #endregion

    #region General
    public static void GenerateSquares(Transform transform, bool useColumns, LevelPrefabs prefabs, GameObject squarePrefab,
        Vector3[,] coordinates, Maze2DSquare[,] squares)
    {
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            GameObject row = new GameObject("Row" + (i + 1));
            row.transform.parent = transform;
            row.transform.position = transform.position;

            for (int j = 0; j < squares.GetLength(1); j++)
            {
                if (squares[i, j] == null)
                    continue;

                Survey(i, j, squares);

                #region Setup Square in Hierarchy
                squares[i, j].layout = BuildLayout(useColumns, prefabs, squarePrefab, squares[i, j].adjacents);
                GameObject sq = squares[i, j].layout;

                sq.transform.parent = transform;
                sq.transform.localPosition = coordinates[i, j];
                sq.transform.parent = row.transform;

                sq.name = "Sq" + (j + 1);
                #endregion
            }
        }
    }

    public static Vector3[,] GenerateCoordinates(float incrementX, float incrementY, Maze2DSquare[,] grid)
    {
        Vector3[,] coordinates = new Vector3[grid.GetLength(0), grid.GetLength(1)];

        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                    coordinates[i, j] = new Vector3(j * incrementX, 0, i * incrementY);

        return coordinates;
    }

    public static void SetOutlet(bool useColumns, LevelPrefabs prefabs, GameObject outlet, out Maze2DSquare outletSq, Maze2DSquare[,] squares)
    {
        List<Maze2DSquare> vacancyList = new List<Maze2DSquare>();
        vacancyList = FillVacancyList(vacancyList, squares);

        int outletIndex = Random.Range(0, vacancyList.Count);
        Maze2DSquare square = vacancyList[outletIndex];
        Maze2DSquare tempSq;

        //Identify and store the root square's open adjacent squares
        List<int> indices = new List<int>();
        List<Maze2DSquare> openAdjacents = new List<Maze2DSquare>();
        
        for (int i = 0; i < square.adjacents.Length; i++)
        {
            if (square.adjacents[i] == null)
            {
                openAdjacents.Add(square.adjacents[i]);
                indices.Add(i);
            }
        }

        int rand = Random.Range(0, openAdjacents.Count);
        SurveyOutlet(square, indices[rand], squares, out tempSq);
        outletSq = tempSq;

        //Replace wall with outlet;
        if (square.layout.transform.GetChild(indices[rand]).childCount > 0)
            Object.Destroy(square.layout.transform.GetChild(indices[rand]).GetChild(0).gameObject);
        outlet = Object.Instantiate(outlet, square.layout.transform.GetChild(indices[rand]));
        ZeroOut(outlet);
        outletSq.layout = outlet;

        //Add columns
        if (useColumns && prefabs.column != null)
        {
            Transform t = square.layout.transform;
            GameObject col = Object.Instantiate(prefabs.column, t.GetChild(indices[rand]));
            ZeroOut(col);
            col = (indices[rand] < 3) ? Object.Instantiate(prefabs.column, t.GetChild(indices[rand] + 1)) :
                Object.Instantiate(prefabs.column, t.GetChild(0));
            ZeroOut(col);
        }

        if (vacancyList.Count > 1)
            vacancyList.RemoveAt(outletIndex);
    }

    public static GameObject BuildLayout(bool useColumns, LevelPrefabs prefabs, GameObject sqPrefab, Maze2DSquare[] adjacents)
    {
        int num2D = 4;

        GameObject output = Object.Instantiate(sqPrefab);

        //Store the four corners
        Transform[] corners = new Transform[num2D];
        for (int i = 0; i < corners.Length; i++)
            corners[i] = output.transform.GetChild(i);

        for (int i = 0; i < adjacents.Length; i++)
        {
            Maze2DSquare next = adjacents[i];
            Maze2DSquare previous = (i > 0) ? adjacents[i - 1] : adjacents[num2D - 1];
            GameObject edgePrefab = prefabs.wall;

            if (next != null && previous != null && useColumns)
                edgePrefab = prefabs.column;
            else if (next != null)
                continue;

            if (edgePrefab != null)
                Object.Instantiate(edgePrefab, corners[i]);
        }
        if (prefabs.ceiling != null)
            Object.Instantiate(prefabs.ceiling, output.transform.GetChild(4));
        if (prefabs.floor != null)
            Object.Instantiate(prefabs.floor, output.transform.GetChild(5));

        return output;
    }

    public static void ZeroOut(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
    }

    static List<Maze2DSquare> FillVacancyList(List<Maze2DSquare> list, Maze2DSquare[,] grid)
    {
        list.Clear();

        //Add all squares with an open adjacent
        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                if (grid[i, j] != null && !grid[i, j].isOutlet)
                    for (int k = 0; k < grid[i, j].adjacents.Length; k++)
                        if (grid[i, j].adjacents[k] == null)
                        {
                            list.Add(grid[i, j]);
                            break;
                        }

        return list;
    }

    public static void ClearGrid(Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
            Object.Destroy(transform.GetChild(i).gameObject);
    }
    #endregion

    #region Clustering

    public static void WriteClusters(float gapFrequency, out List<List<Maze2DSquare[]>> gridClusters, Maze2DSquare[,] squares)
    {
        gridClusters = new List<List<Maze2DSquare[]>>();
        int length = squares.GetLength(0);
        int width = squares.GetLength(1);

        for (int i = 0; i < length; i++)
        {
            List<Maze2DSquare[]> rowClusters = new List<Maze2DSquare[]>();
            int clusterSize = 0;

            for (int j = 0; j < width; j++)
            {
                Maze2DSquare[] cluster;

                //Only 1 cluster for the origin
                if (i == 0 && j == 0)
                {
                    squares[i, j] = new Maze2DSquare(i, j);
                    clusterSize++;
                    continue;
                }

                //Store clusters
                float rand = Random.Range(0, 1f);
                if (rand < gapFrequency)
                {
                    if (clusterSize > 0)
                    {
                        //Cluster excluding current (null) square
                        cluster = SetCluster(clusterSize, i, j, squares);
                        rowClusters.Add(cluster);
                        clusterSize = 0;
                    }

                    if (i == 0)
                        break;
                    continue;
                }

                squares[i, j] = new Maze2DSquare(i, j);

                clusterSize++;
                if (j == width - 1)
                {
                    //Cluster including current (!null) square
                    cluster = SetCluster(clusterSize, i, j + 1, squares);
                    rowClusters.Add(cluster);
                }
            }

            gridClusters.Add(rowClusters);
        }

        LinkByClusters(gridClusters, squares);
    }

    static Maze2DSquare[] SetCluster(int size, int col, int row, Maze2DSquare[,] grid)
    {
        Maze2DSquare[] cluster = new Maze2DSquare[size];

        for (int i = 0; i < size; i++)
            cluster[i] = grid[col, row - (size - i)];

        return cluster;
    }

    static void LinkByClusters(List<List<Maze2DSquare[]>> gridClusters, Maze2DSquare[,] squares)
    {
        for (int i = 1; i < squares.GetLength(0); i++)
        {
            //Iterate through clusters
            for (int j = 0; j < gridClusters[i].Count; j++)
            {
                int connections = 0;

                for (int k = 0; k < gridClusters[i][j].Length; k++)
                {
                    Maze2DSquare current = gridClusters[i][j][k];

                    if (squares[i - 1, current.yCoord] != null)
                        connections++;
                }

                if (connections == 0)
                {
                    //Scout out to the first row to find where this cluster can connect
                    bool hit = false;

                    for (int k = 0; k < gridClusters[i][j].Length; k++)
                    {
                        Maze2DSquare current = gridClusters[i][j][k];

                        for (int l = i - 1; l >= 0; l--)
                        {
                            if (squares[l, current.yCoord] != null)
                            {
                                hit = true;
                                for (int m = i - 1; m > l; m--)
                                    squares[m, current.yCoord] = new Maze2DSquare(m, current.yCoord);

                                break;
                            }
                        }

                        if (hit == true)
                            break;
                    }

                    //If there's nowhere it can connect, draw a line down, then to the first row, then to the origin
                    if (hit == false)
                    {
                        Maze2DSquare sq = gridClusters[i][j][0];

                        for (int k = i - 1; k >= 0; k--)
                            squares[k, sq.yCoord] = new Maze2DSquare(k, sq.yCoord);

                        for (int k = sq.yCoord; k >= gridClusters[0][0].Length; k--)
                            if (squares[0, k] == null)
                                squares[0, k] = new Maze2DSquare(0, k);
                    }
                }
            }
        }
    }
    #endregion
}