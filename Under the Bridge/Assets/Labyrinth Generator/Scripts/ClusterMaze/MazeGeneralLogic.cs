using System.Collections.Generic;
using UnityEngine;

public static class MazeGeneralLogic
{
    const int CORNERS = 4;
    const int SIDES = 6;

    public static void GenerateSquares(Transform transform, bool useColumns, LevelPrefabs prefabs, GameObject squarePrefab,
        Vector3[,,] coordinates, MazeSquare[,,] squares)
    {
        for (int z = 0; z < squares.GetLength(2); z++) {
            GameObject layer = NewSection("Layer", z, transform);

            for (int i = 0; i < squares.GetLength(0); i++) {
                GameObject row = NewSection("Row", i, layer.transform);

                for (int j = 0; j < squares.GetLength(1); j++) {
                    if (squares[i, j, z] == null)
                        continue;

                    MazeSurveyLogic.Survey(i, j, z, squares);

                    #region Setup Square in Hierarchy
                    squares[i, j, z].layout = BuildLayout(useColumns, prefabs, squarePrefab, squares[i, j, z].adjacents);
                    GameObject sq = squares[i, j, z].layout;

                    sq.transform.parent = transform;
                    sq.transform.localPosition = coordinates[i, j, z];
                    sq.transform.parent = row.transform;

                    sq.name = "Sq" + (j + 1);
                    #endregion
                }
            }
        }
    }

    static GameObject NewSection(string sectionName, int num, Transform parent)
    {
        GameObject section = new GameObject(sectionName + (num + 1));
        section.transform.parent = parent;
        section.transform.position = parent.position;

        return section;
    }

    public static Vector3[,,] GenerateCoordinates(float incrementX, float incrementY, float incrementZ, MazeSquare[,,] grid)
    {
        Vector3[,,] coordinates = new Vector3[grid.GetLength(0), grid.GetLength(1), grid.GetLength(2)];

        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                for (int z = 0; z < grid.GetLength(2); z++)
                    coordinates[i, j, z] = new Vector3(j * incrementX, z * incrementZ, i * incrementY);

        return coordinates;
    }

    public static void SetOutlet(bool useColumns, LevelPrefabs prefabs, GameObject outlet, out MazeSquare outletSq, MazeSquare[,,] squares)
    {
        List<MazeSquare> vacancyList = new List<MazeSquare>();
        vacancyList = FillVacancyList(vacancyList, squares);

        int outletIndex = Random.Range(0, vacancyList.Count);
        MazeSquare square = vacancyList[outletIndex];
        MazeSquare tempSq;

        //Identify and store the root square's open adjacent squares
        List<int> indices = new List<int>();
        List<MazeSquare> openAdjacents = new List<MazeSquare>();

        for (int i = 0; i < CORNERS; i++) {
            if (square.adjacents[i] == null) {
                openAdjacents.Add(square.adjacents[i]);
                indices.Add(i);
            }
        }

        int rand = Random.Range(0, openAdjacents.Count);
        MazeSurveyLogic.SurveyOutlet(square, indices[rand], squares, out tempSq);
        outletSq = tempSq;

        //Replace wall with outlet
        Transform wall = square.layout.transform.GetChild(indices[rand]);
        Object.Destroy(wall.GetChild(0).gameObject);
        outlet = Object.Instantiate(outlet, wall);
        ZeroOut(outlet);
        outletSq.layout = outlet;

        if (vacancyList.Count > 1)
            vacancyList.RemoveAt(outletIndex);
    }

    static GameObject BuildLayout(bool useColumns, LevelPrefabs prefabs, GameObject sqPrefab, MazeSquare[] adjacents)
    {
        GameObject output = Object.Instantiate(sqPrefab);

        //Store the four corners
        Transform[] corners = new Transform[CORNERS];
        for (int i = 0; i < corners.Length; i++)
            corners[i] = output.transform.GetChild(i);
        
        for (int i = 0; i < CORNERS; i++)
        {
            MazeSquare next = adjacents[i];
            MazeSquare previous = (i > 0) ? adjacents[i - 1] : adjacents[CORNERS - 1];
            GameObject edgePrefab = prefabs.wall;

            //Columns. If two adjacent sides have MazeSquares, put a column on the corner.
            if (useColumns && next != null && previous != null)
            {
                //Prevent duplication. Maze construction goes west to east and south to north, so check west adjacent square
                //  and south adjacent square for where they could already have columns.
                if (i == 0 && previous.adjacents[0] != null)
                    continue;
                else if (i > 1 && next.adjacents[i - 1] != null)
                    continue;

                edgePrefab = prefabs.column;
            }
            else if (next != null)
                continue;

            Object.Instantiate(edgePrefab, corners[i]);
        }

        //4 is up and 5 is down
        if (adjacents[4] == null || adjacents[4].isOutlet)
            Object.Instantiate(prefabs.ceiling, output.transform.GetChild(4));
        if (adjacents[5] == null || adjacents[5].isOutlet)
            Object.Instantiate(prefabs.floor, output.transform.GetChild(5));

        return output;
    }

    public static void LinkPortalPair(Portal portal1, Portal portal2)
    {
        portal1.destination = portal2;
        portal2.destination = portal1;
    }

    public static void ZeroOut(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
    }

    static List<MazeSquare> FillVacancyList(List<MazeSquare> list, MazeSquare[,,] grid)
    {
        list.Clear();

        //Add all squares with an open adjacent
        for (int z = 0; z < grid.GetLength(2); z++)
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                    if (grid[i, j, z] != null && !grid[i, j, z].isOutlet)
                        for (int k = 0; k < CORNERS; k++)
                            if (grid[i, j, z].adjacents[k] == null) {
                                list.Add(grid[i, j, z]);
                                break;
                            }

        return list;
    }

    public static void ClearGrid(Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
            Object.Destroy(transform.GetChild(i).gameObject);
    }
}