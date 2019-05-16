using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SprawlGeneratorLogic
{
    static Vector3[] directions3D = new Vector3[] {
        Vector3.forward, Vector3.back,
        Vector3.left, Vector3.right,
        Vector3.up, Vector3.down
    };

    static Vector3[] directions2D = new Vector3[] {
        Vector3.forward, Vector3.back,
        Vector3.left, Vector3.right,
    };

    public static List<SprawlerSquare> Generate(SprawlerSquare[] outlets, bool isFlat, int numSquares, Vector3 spread, float turnFrequency, float deadEndFrequency)
    {
        List<SprawlerSquare> squares = new List<SprawlerSquare>();
        List<Vector3> allCoordinates = new List<Vector3>();

        Vector3 newCoordinates;
        SprawlerSquare newSquare;

        Vector3[] directions = isFlat ? directions2D : directions3D;

        int capacity = Capacity(spread, isFlat);

        if (capacity <= numSquares)
        {
            squares = FillSpread(spread, squares);

            for (int i = 0; i < squares.Count; i++)
                allCoordinates.Add(squares[i].coordinates);
        }
        else
        {
            //Origin square
            newCoordinates = Vector3.zero;
            newSquare = new SprawlerSquare(newCoordinates);
            squares.Add(newSquare);
            allCoordinates.Add(newCoordinates);

            Vector3 direction = Direct(directions);
            float turn = 1;
            float deadEnd = 1;

            for (int i = 1; i < numSquares; i++)
            {
                turn = Random.Range(0, 1f);
                deadEnd = Random.Range(0, 1f);

                //Direction change
                if (turn < turnFrequency)
                    direction = Direct(directions);

                //Dead end
                if (deadEnd < deadEndFrequency)
                {
                    newCoordinates = allCoordinates[Random.Range(0, allCoordinates.Count)];
                    direction = Direct(directions);
                }

                newCoordinates += direction;

                //If hit another block or boundary
                if (allCoordinates.Contains(newCoordinates) || HitBoundary(newCoordinates, spread))
                {
                    Vector3 backtrack = newCoordinates - direction;
                    Vector3 dir = Redirect(backtrack, allCoordinates, directions, spread);

                    //Anywhere to go?
                    if (dir == Vector3.zero)
                    {
                        newCoordinates = allCoordinates[Random.Range(0, allCoordinates.Count)];
                        direction = Direct(directions);
                    }
                    else
                    {
                        direction = dir;
                        newCoordinates = backtrack + dir;
                    }
                }

                newSquare = new SprawlerSquare(newCoordinates);

                squares.Add(newSquare);
                allCoordinates.Add(newCoordinates);
            }
        }

        //Store adjacent squares
        StoreAdjacents(squares, allCoordinates);

        //Add outlets
        squares = AddOutlets(outlets, squares);

        return squares;
    }

    static List<SprawlerSquare> FillSpread(Vector3 spread, List<SprawlerSquare> squares)
    {
        int spreadX = (int)spread.x;
        int spreadY = (int)spread.y;
        int spreadZ = (int)spread.z;

        for (int i = -spreadX; i <= spreadX; i++)
            for (int j = -spreadY; j <= spreadY; j++)
                for (int k = -spreadZ; k <= spreadZ; k++)
                    squares.Add(new SprawlerSquare(new Vector3(i, j, k)));

        return squares;
    }

    static List<SprawlerSquare> AddOutlets(SprawlerSquare[] outlets, List<SprawlerSquare> squares)
    {
        List<Vector3> OutletCoordinates = new List<Vector3>();
        foreach (SprawlerSquare square in squares)
            for (int i = 0; i < directions2D.Length; i++)
                if (square.adjacents[i] == null)
                    OutletCoordinates.Add(square.coordinates + directions2D[i]);

        for (int i = 0; i < outlets.Length; i++)
        {
            outlets[i] = new SprawlerSquare(OutletCoordinates[Random.Range(0, OutletCoordinates.Count)]);

            OutletCoordinates.Remove(outlets[i].coordinates);

            squares.Add(outlets[i]);
        }

        return squares;
    }

    static void StoreAdjacents(List<SprawlerSquare> squares, List<Vector3> allCoordinates)
    {
        foreach (SprawlerSquare square in squares)
        {
            square.adjacents = new SprawlerSquare[directions3D.Length];

            for (int i = 0; i < directions3D.Length; i++)
            {
                Vector3 adjCoords = square.coordinates + directions3D[i];

                if (allCoordinates.Contains(adjCoords))
                    square.adjacents[i] = squares[allCoordinates.IndexOf(adjCoords)];
            }
        }
    }

    static Vector3 Direct(Vector3[] directions)
    {
        return (directions[Random.Range(0, directions.Length)]);
    }
    
    static Vector3 Redirect(Vector3 currentCoords, List<Vector3> allCoords, Vector3[] directions, Vector3 spread)
    {
        List<Vector3> openings = new List<Vector3>();

        foreach(Vector3 dir in directions)
            if (allCoords.Contains(currentCoords + dir) ||
                currentCoords.x > spread.x || currentCoords.y > spread.y || currentCoords.z > spread.z)
                continue;
            else
                openings.Add(dir);

        if (openings.Count > 0)
            return (openings[Random.Range(0, openings.Count)]);

        return (Vector3.zero);
    }

    static bool HitBoundary(Vector3 coordinates, Vector3 spread)
    {
        int hitX = (int)Mathf.Abs(coordinates.x);
        int hitY = (int)Mathf.Abs(coordinates.y);
        int hitZ = (int)Mathf.Abs(coordinates.z);

        if (hitX >=  spread.x ||
            hitY >= spread.y ||
            hitZ >= spread.z)
            return true;
        else
            return false;
    }

    static int Capacity(Vector3 spread, bool isFlat)
    {
        int x = 1 + ((int)Mathf.Abs(spread.x) * 2);
        int y = isFlat ? 1 : 1 + ((int)Mathf.Abs(spread.y) * 2);
        int z = 1 + ((int)Mathf.Abs(spread.z) * 2);

        int capacity = x * y * z;

        return capacity;
    }

    public static GameObject BuildSquare(SprawlerSquare square, LevelPrefabs prefabs, GameObject sqPrefab)
    {
        GameObject output = Object.Instantiate(sqPrefab);

        GameObject currentPrefab;

        for (int i = 0; i < square.adjacents.Length; i++)
        {
            currentPrefab = i < 4 ? prefabs.wall :
                i == 4 ? prefabs.ceiling : prefabs.floor;

            if (square.adjacents[i] == null)
                Object.Instantiate(currentPrefab, output.transform.GetChild(i));
        }

        return output;
    }

    public static Vector3 SquarePosition(SprawlerSquare square, PrefabDimensions dimensions)
    {
        Vector3 position = new Vector3(
            square.coordinates.x * dimensions.width,
            square.coordinates.y * dimensions.height,
            square.coordinates.z * dimensions.length
            );

        return position;
    }
}
