using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LinkingLogic {

    public static Dictionary<int, int[]> directions = new Dictionary<int, int[]> {
            { 0, new int[] {1,0,0} },
            { 1, new int[] {0,1,0} },
            { 2, new int[] {-1,0,0} },
            { 3, new int[] {0,-1,0} },
            { 4, new int[] {0,0,1} },
            { 5, new int[] {0,0,-1} }
        };

    static Dictionary<int, int> adjDirectionFlips = new Dictionary<int, int> { { 0, 2 }, { 1, 3 }, { 2, 0 }, { 3, 1 }, { 4, 5 }, { 5, 4 } };

    public static void Survey(int rowIndex, int colIndex, DungeonSquare[,] grid)
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

    public static void Survey(int rowIndex, int colIndex, int layIndex, DungeonSquare[,,] grid)
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

    public static void SurveyOutlet(DungeonSquare rootSquare, int adjacencyIndex, DungeonSquare[,] grid, out DungeonSquare outletSquare)
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

    public static void SurveyOutlet(DungeonSquare rootSquare, int adjacencyIndex, DungeonSquare[,,] grid, out DungeonSquare outletSquare)
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

    public static void ZeroOut(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
    }
}
