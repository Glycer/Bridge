using System.Collections.Generic;

public static class MazeSurveyLogic
{
    public static Dictionary<int, int[]> directions = new Dictionary<int, int[]> {
            { 0, new int[] {1,0,0} },
            { 1, new int[] {0,1,0} },
            { 2, new int[] {-1,0,0} },
            { 3, new int[] {0,-1,0} },
            { 4, new int[] {0,0,1} },
            { 5, new int[] {0,0,-1} }
        };

    public static Dictionary<int, int> adjDirectionFlips = new Dictionary<int, int> { { 0, 2 }, { 1, 3 }, { 2, 0 }, { 3, 1 }, { 4, 5 }, { 5, 4 } };
    
    public static void Survey(int rowIndex, int colIndex, int layIndex, MazeSquare[,,] grid)
    {
        MazeSquare square = grid[rowIndex, colIndex, layIndex];

        #region Cardinal Directions
        MazeSquare north = (rowIndex < grid.GetLength(0) - 1) ? grid[rowIndex + 1, colIndex, layIndex] : null;
        MazeSquare east = (colIndex < grid.GetLength(1) - 1) ? grid[rowIndex, colIndex + 1, layIndex] : null;
        MazeSquare south = (rowIndex != 0) ? grid[rowIndex - 1, colIndex, layIndex] : null;
        MazeSquare west = (colIndex != 0) ? grid[rowIndex, colIndex - 1, layIndex] : null;
        MazeSquare up = (layIndex < grid.GetLength(2) - 1) ? grid[rowIndex, colIndex, layIndex + 1] : null;
        MazeSquare down = (layIndex != 0) ? grid[rowIndex, colIndex, layIndex - 1] : null;
        #endregion

        MazeSquare[] adjacentSquares = new MazeSquare[] { north, east, south, west, up, down };
        square.adjacents = adjacentSquares;
    }

    public static void SurveyOutlet(MazeSquare rootSquare, int adjacencyIndex, MazeSquare[,,] grid, out MazeSquare outletSquare)
    {
        rootSquare.adjacents[adjacencyIndex] = new MazeSquare(
            rootSquare.xCoord + directions[adjacencyIndex][0],
            rootSquare.yCoord + directions[adjacencyIndex][1],
            rootSquare.zCoord + directions[adjacencyIndex][2]
            );

        outletSquare = rootSquare.adjacents[adjacencyIndex];
        outletSquare.isOutlet = true;

        if (outletSquare.xCoord >= 0 && outletSquare.xCoord < grid.GetLength(0) &&
            outletSquare.yCoord >= 0 && outletSquare.yCoord < grid.GetLength(1) &&
            outletSquare.zCoord >= 0 && outletSquare.zCoord < grid.GetLength(2)) {

            grid[outletSquare.xCoord, outletSquare.yCoord, outletSquare.zCoord] = outletSquare;
            Survey(outletSquare.xCoord, outletSquare.yCoord, outletSquare.zCoord, grid);

            //Fill open slot of any square adjacent to this new outlet
            for (int i = 0; i < outletSquare.adjacents.Length; i++)
                if (outletSquare.adjacents[i] != null)
                    outletSquare.adjacents[i].adjacents[adjDirectionFlips[i]] = outletSquare;
        }
    }
}