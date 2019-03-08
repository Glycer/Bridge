using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLogic : MonoBehaviour
{
    public GameObject dungeonSquare;
    public bool useColumns = true;

    public DungeonSquare entranceSq;
    public DungeonSquare exitSq;

    LevelPrefabs levelPrefabs;

    public static Dictionary<int, int[]> directions = new Dictionary<int, int[]> {
            { 0, new int[] {1,0,0} },
            { 1, new int[] {0,1,0} },
            { 2, new int[] {-1,0,0} },
            { 3, new int[] {0,-1,0} },
            { 4, new int[] {0,0,1} },
            { 5, new int[] {0,0,-1} }
        };

    Dictionary<int, int> adjDirectionFlips = new Dictionary<int, int> { {0, 2}, {1, 3}, {2, 0}, {3, 1}, {4, 5}, {5, 4} };

    private void Start()
    {
        levelPrefabs = GetComponent<LevelPrefabs>();
    }

    public void Survey(int rowIndex, int colIndex, DungeonSquare[,] grid)
    {
        DungeonSquare square = grid[rowIndex, colIndex];

        #region Cardinal Directions
        DungeonSquare north = (rowIndex < grid.GetLength(0) - 1) ? grid[rowIndex + 1, colIndex] : null;
        DungeonSquare east = (colIndex < grid.GetLength(1) - 1) ? grid[rowIndex, colIndex + 1] : null;
        DungeonSquare south = (rowIndex != 0) ? grid[rowIndex - 1, colIndex] : null;
        DungeonSquare west = (colIndex != 0) ? grid[rowIndex, colIndex - 1] : null;
        #endregion

        DungeonSquare[] adjacentSquares = new DungeonSquare[] { north, east, south, west };
        square.adjacents = adjacentSquares;
    }

    public void Survey(int rowIndex, int colIndex, int layIndex, DungeonSquare[,,] grid)
    {
        DungeonSquare square = grid[rowIndex, colIndex, layIndex];

        #region Cardinal Directions
        DungeonSquare north = (rowIndex < grid.GetLength(0) - 1) ? grid[rowIndex + 1, colIndex, layIndex] : null;
        DungeonSquare east = (colIndex < grid.GetLength(1) - 1) ? grid[rowIndex, colIndex + 1, layIndex] : null;
        DungeonSquare south = (rowIndex != 0) ? grid[rowIndex - 1, colIndex, layIndex] : null;
        DungeonSquare west = (colIndex != 0) ? grid[rowIndex, colIndex - 1, layIndex] : null;
        DungeonSquare up = (layIndex < grid.GetLength(2)) ? grid[rowIndex, colIndex, layIndex + 1] : null;
        DungeonSquare down = (layIndex != 0) ? grid[rowIndex, colIndex, layIndex - 1] : null;
        #endregion

        DungeonSquare[] adjacentSquares = new DungeonSquare[] { north, east, south, west, up, down };
        square.adjacents = adjacentSquares;
    }

    public void AttachOutlet(DungeonSquare square, GameObject outlet, DungeonSquare[,] grid, out DungeonSquare outletSquare)
    {
        //Identify and store the root square's open adjacent squares
        List<int> indices = new List<int>();
        List<DungeonSquare> openAdjacents = new List<DungeonSquare>();

        for (int i = 0; i < square.adjacents.Length; i++)
        {
            if (square.adjacents[i] == null)
            {
                openAdjacents.Add(square.adjacents[i]);
                indices.Add(i);
            }
        }

        int rand = Random.Range(0, openAdjacents.Count);
        SurveyOutlet(square, indices[rand], grid, out outletSquare);

        //Replace wall with outlet
        Destroy(square.layout.transform.GetChild(indices[rand]).GetChild(0).gameObject);
        outlet = Instantiate(outlet, square.layout.transform.GetChild(indices[rand]));
        ZeroOut(outlet);
        outletSquare.layout = outlet;

        //Add columns
        if (useColumns)
        {
            Transform t = square.layout.transform;
            GameObject col = Instantiate(levelPrefabs.column, t.GetChild(indices[rand]));
            ZeroOut(col);
            col = (indices[rand] < 3) ? Instantiate(levelPrefabs.column, t.GetChild(indices[rand] + 1)) : Instantiate(levelPrefabs.column, t.GetChild(0));
            ZeroOut(col);
        }
    }

    public void AttachOutlet(DungeonSquare square, GameObject outlet, DungeonSquare[,,] grid, out DungeonSquare outletSquare)
    {
        //Identify and store the root square's open adjacent squares
        List<int> indices = new List<int>();
        List<DungeonSquare> openAdjacents = new List<DungeonSquare>();

        for (int i = 0; i < square.adjacents.Length; i++)
        {
            if (square.adjacents[i] == null)
            {
                openAdjacents.Add(square.adjacents[i]);
                indices.Add(i);
            }
        }

        int rand = Random.Range(0, openAdjacents.Count);
        SurveyOutlet(square, indices[rand], grid, out outletSquare);

        //Replace wall with outlet
        Destroy(square.layout.transform.GetChild(indices[rand]).GetChild(0).gameObject);
        outlet = Instantiate(outlet, square.layout.transform.GetChild(indices[rand]));
        ZeroOut(outlet);
        outletSquare.layout = outlet;

        //Add columns
        if (useColumns)
        {
            Transform t = square.layout.transform;
            GameObject col = Instantiate(levelPrefabs.column, t.GetChild(indices[rand]));
            ZeroOut(col);
            col = (indices[rand] < 3) ? Instantiate(levelPrefabs.column, t.GetChild(indices[rand] + 1)) : Instantiate(levelPrefabs.column, t.GetChild(0));
            ZeroOut(col);
        }
    }

    void SurveyOutlet(DungeonSquare rootSquare, int adjacencyIndex, DungeonSquare[,] grid, out DungeonSquare outletSquare)
    {
        rootSquare.adjacents[adjacencyIndex] = new DungeonSquare(
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
            {
                if (outletSquare.adjacents[i] != null)
                {
                    outletSquare.adjacents[i].adjacents[adjDirectionFlips[i]] = outletSquare;
                }
            }
        }
    }

    void SurveyOutlet(DungeonSquare rootSquare, int adjacencyIndex, DungeonSquare[,,] grid, out DungeonSquare outletSquare)
    {
        rootSquare.adjacents[adjacencyIndex] = new DungeonSquare(
            rootSquare.xCoord + directions[adjacencyIndex][0],
            rootSquare.yCoord + directions[adjacencyIndex][1],
            rootSquare.zCoord + directions[adjacencyIndex][2]
            );

        outletSquare = rootSquare.adjacents[adjacencyIndex];
        outletSquare.isOutlet = true;

        if (outletSquare.xCoord >= 0 && outletSquare.xCoord < grid.GetLength(0) &&
            outletSquare.yCoord >= 0 && outletSquare.yCoord < grid.GetLength(1) &&
            outletSquare.zCoord >= 0 && outletSquare.zCoord < grid.GetLength(2))
        {
            grid[outletSquare.xCoord, outletSquare.yCoord, outletSquare.zCoord] = outletSquare;
            Survey(outletSquare.xCoord, outletSquare.yCoord, outletSquare.zCoord, grid);

            //Fill open slot of any square adjacent to this new outlet
            for (int i = 0; i < outletSquare.adjacents.Length; i++)
            {
                if (outletSquare.adjacents[i] != null)
                {
                    outletSquare.adjacents[i].adjacents[adjDirectionFlips[i]] = outletSquare;
                }
            }
        }
    }

    public GameObject BuildLayout(DungeonSquare[] adjacents)
    {
        int num2D = 4;

        GameObject output = Instantiate(dungeonSquare);
        //2D
        if (adjacents.Length == num2D)
        {
            Instantiate(levelPrefabs.floor, output.transform);
            Instantiate(levelPrefabs.ceiling, output.transform.GetChild(4));
        }

        //Store the four corners
        Transform[] corners = new Transform[num2D];
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = output.transform.GetChild(i);
        }

        for (int i = 0; i < adjacents.Length; i++)
        {
            if (i < num2D)
            {
                DungeonSquare next = adjacents[i];
                DungeonSquare previous = (i > 0) ? adjacents[i - 1] : adjacents[num2D - 1];
                GameObject edgePrefab = levelPrefabs.wall;

                if (next != null && previous != null && useColumns)
                    edgePrefab = levelPrefabs.column;
                else if (next != null)
                    continue;

                Instantiate(edgePrefab, corners[i]);
            }
            else
            {
                //4 is up and 5 is down
                if (i == 4 && adjacents[i] == null)
                    Instantiate(levelPrefabs.ceiling, output.transform);
                else if (i == 5 && adjacents[i] == null)
                    Instantiate(levelPrefabs.floor, output.transform);
            }
        }

        return output;
    }

    void ZeroOut(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
    }
}