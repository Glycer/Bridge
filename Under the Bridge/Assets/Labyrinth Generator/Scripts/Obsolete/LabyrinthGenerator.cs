using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGenerator : MonoBehaviour {

    public int width;
    public int length;

    public float gapFrequency;

    Vector3[,] coordinates;
    DungeonSquare[,] squares;

    ConnectLogic connectLogic;
    LevelPrefabs levelPrefabs;

    private void Start()
    {
        connectLogic = GetComponent<ConnectLogic>();
        levelPrefabs = GetComponent<LevelPrefabs>();
    }

    void GenerateCoordinates(float incrementX, float incrementY, DungeonSquare[,] grid)
    {
        coordinates = new Vector3[grid.GetLength(0), grid.GetLength(1)];

        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                coordinates[i, j] = new Vector3(j * incrementX, 0, i * incrementY);
            }
    }

    public void GenerateGrid()
    {
        ClearGrid();
        squares = new DungeonSquare[length, width];
        PrefabDimensions dimensions = connectLogic.dungeonSquare.GetComponent<PrefabDimensions>();
        GenerateCoordinates(dimensions.width, dimensions.length, squares);
        
        //[Column][Cluster][Square]
        List<List<DungeonSquare[]>> gridClusters = new List<List<DungeonSquare[]>>();
        
        for (int i = 0; i < length; i++)
        {
            List<DungeonSquare[]> rowClusters = new List<DungeonSquare[]>();
            int clusterSize = 0;

            for (int j = 0; j < width; j++)
            {
                #region Clustering
                DungeonSquare[] cluster;

                //Only 1 cluster for the origin
                if (i == 0 && j == 0)
                {
                    squares[i, j] = new DungeonSquare(i, j);
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

                squares[i, j] = new DungeonSquare(i, j);

                clusterSize++;
                if (j == width - 1)
                {
                    //Cluster including current (!null) square
                    cluster = SetCluster(clusterSize, i, j+1, squares);
                    rowClusters.Add(cluster);
                }
                #endregion
            }

            gridClusters.Add(rowClusters);
        }

        #region Linking
        for (int i = 1; i < length; i++)
        {
            //Iterate through clusters
            for (int j = 0; j < gridClusters[i].Count; j++)
            {
                int connections = 0;

                for (int k = 0; k < gridClusters[i][j].Length; k++)
                {
                    DungeonSquare current = gridClusters[i][j][k];
                    
                    if (squares[i - 1, current.yCoord] != null)
                        connections++;
                }

                if (connections == 0)
                {
                    //Scout out to the first row to find where this cluster can connect
                    bool hit = false;

                    for (int k = 0; k < gridClusters[i][j].Length; k++)
                    {
                        DungeonSquare current = gridClusters[i][j][k];

                        for (int l = i-1; l >= 0; l--)
                        {
                            if (squares[l, current.yCoord] != null)
                            {
                                hit = true;
                                for (int m = i-1; m > l; m--)
                                {
                                    squares[m, current.yCoord] = new DungeonSquare(m, current.yCoord);
                                }
                                break;
                            }
                        }

                        if (hit == true)
                            break;
                    }

                    //If there's nowhere it can connect, draw a vertical line, then horizontal to the origin
                    if (hit == false)
                    {
                        DungeonSquare sq = gridClusters[i][j][0];

                        for (int k = i - 1; k >= 0; k--)
                        {
                            squares[k, sq.yCoord] = new DungeonSquare(k, sq.yCoord);
                        }
                        
                        for (int k = sq.yCoord; k >= gridClusters[0][0].Length; k--)
                        {
                            if (squares[0, k] == null)
                                squares[0, k] = new DungeonSquare(0, k);
                        }
                    }
                }
            }
        }
        #endregion

        GenerateSquares(squares);
        GenerateOutlets(squares);
    }

    DungeonSquare[] SetCluster(int size, int col, int row, DungeonSquare[,] grid)
    {
        DungeonSquare[] cluster = new DungeonSquare[size];

        for (int i = 0; i < size; i++)
        {
            cluster[i] = grid[col, row - (size - i)];
        }

        return cluster;
    }

    void GenerateSquares(DungeonSquare[,] squares)
    {
        for (int i = 0; i < length; i++)
        {
            GameObject row = new GameObject("Row" + (i + 1));
            row.transform.parent = transform;
            row.transform.position = transform.position;

            for (int j = 0; j < width; j++)
            {
                if (squares[i, j] == null)
                    continue;

                connectLogic.Survey(i, j, squares);

                #region Setup Square in Hierarchy
                squares[i, j].layout = connectLogic.BuildLayout(squares[i, j].adjacents);
                GameObject sq = squares[i, j].layout;

                sq.transform.parent = transform;
                sq.transform.localPosition = coordinates[i, j];
                sq.transform.parent = row.transform;

                sq.name = "Sq" + (j + 1);
                #endregion
            }
        }
    }

    void ClearGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void GenerateOutlets(DungeonSquare[,] grid)
    {
        List<DungeonSquare> vacancyList = new List<DungeonSquare>();

        //Pick two and make them the entrance and exit
        vacancyList = RefreshVacancyList(vacancyList, grid);
        SetOutlet(vacancyList, levelPrefabs.entrance, out connectLogic.entranceSq);

        vacancyList = RefreshVacancyList(vacancyList, grid);
        SetOutlet(vacancyList, levelPrefabs.exit, out connectLogic.exitSq);
    }

    void SetOutlet(List<DungeonSquare> linkables, GameObject outlet, out DungeonSquare sq)
    {
        int outletIndex = Random.Range(0, linkables.Count);
        connectLogic.AttachOutlet(linkables[outletIndex], outlet, squares, out sq);
        if (linkables.Count > 1)
            linkables.RemoveAt(outletIndex);
    }

    List<DungeonSquare> RefreshVacancyList(List<DungeonSquare> list, DungeonSquare[,] grid)
    {
        list.Clear();

        //Add all squares with an open adjacent
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] != null && !grid[i, j].isOutlet)
                {
                    for (int k = 0; k < grid[i, j].adjacents.Length; k++)
                    {
                        if (grid[i, j].adjacents[k] == null)
                        {
                            list.Add(grid[i, j]);
                            break;
                        }
                    }
                }
            }
        }

        return list;
    }
}