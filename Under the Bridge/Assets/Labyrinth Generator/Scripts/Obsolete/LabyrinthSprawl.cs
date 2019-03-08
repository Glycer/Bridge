using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthSprawl : MonoBehaviour {

    public int numberOfSquares;
    public bool sprawl;
    float squareSize = 10;

    List<DungeonSquare> vacancies = new List<DungeonSquare>();

    Dictionary<int, DungeonSquare> coordinatesToSquare = new Dictionary<int, DungeonSquare>();
    Dictionary<int, DungeonSquare> emptyCoordinatesToSquare = new Dictionary<int, DungeonSquare>();

    Dictionary<int, int[]> directions = new Dictionary<int, int[]> {
            { 0, new int[] {0,1} },
            { 1, new int[] {1,0} },
            { 2, new int[] {0,-1} },
            { 3, new int[] {-1,0} }
        };

    ConnectLogic connectLogic;
    //LevelPrefabs levelPrefabs;

    // Use this for initialization
    void Start ()
    {
        connectLogic = GetComponent<ConnectLogic>();
        //levelPrefabs = GetComponent<LevelPrefabs>();

        Generate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            Generate();
    }

    void Generate()
    {
        ClearMaze();

        if (numberOfSquares > 0)
            AddSquare(0,0);
        int num = 1;

        if (sprawl)
            while (num < numberOfSquares)
            {
                vacancies = RefreshVacancies();
                int rand = Random.Range(0, vacancies.Count);

                //If square has full adjacencies, make it less likely to fill
                int adjCount = 1;
                for (int i = 0; i < vacancies[rand].adjacents.Length; i++)
                {
                    if (coordinatesToSquare.ContainsKey(vacancies[rand].adjacents[i].id))
                    {
                        adjCount++;
                        continue;
                    }
                    else
                        break;
                }
                if (adjCount == vacancies[rand].adjacents.Length)
                    continue;

                AddSquare(vacancies[rand].xCoord, vacancies[rand].yCoord);

                num++;
            }
        else
            while (num < numberOfSquares)
            {
                vacancies = RefreshVacancies();
                int rand = Random.Range(0, vacancies.Count);

                AddSquare(vacancies[rand].xCoord, vacancies[rand].yCoord);

                num++;
            }

        foreach (DungeonSquare sq in coordinatesToSquare.Values)
        {
            sq.layout = connectLogic.BuildLayout(sq.adjacents);

            Transform sqTrans = sq.layout.transform;
            sqTrans.parent = transform;
            sqTrans.localPosition = new Vector3(sq.xCoord * squareSize, 0, sq.yCoord * squareSize);
        }
    }

    void ClearMaze()
    {
        coordinatesToSquare.Clear();
        emptyCoordinatesToSquare.Clear();
        vacancies.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void AddSquare(int x, int y)
    {
        DungeonSquare square = new DungeonSquare(x, y);
        
        coordinatesToSquare.Add(square.id, square);
        emptyCoordinatesToSquare.Remove(square.id);

        Survey2D(square);
    }

    void Survey2D(DungeonSquare sq)
    {
        int adjNum = 4;

        sq.adjacents = new DungeonSquare[adjNum];
        int[] adjCoords = new int[adjNum];

        adjCoords[0] = sq.id + 1;
        adjCoords[1] = sq.id + DungeonSquare.ID;
        adjCoords[2] = sq.id - 1;
        adjCoords[3] = sq.id - DungeonSquare.ID;

        for (int i = 0; i < adjNum; i++)
        {
            if (coordinatesToSquare.ContainsKey(adjCoords[i])) //if the adjacent square is in the list of occupied coordinates
                sq.adjacents[i] = coordinatesToSquare[adjCoords[i]]; //reference the square from the list of occupied coordinates as adjacent

            else if (emptyCoordinatesToSquare.ContainsKey(adjCoords[i])) //if the adjacent square is in the list of empties
                sq.adjacents[i] = emptyCoordinatesToSquare[adjCoords[i]]; //reference the square from the list of empties as adjacent

            else //if the adjacent square is in neither list
            {
                sq.adjacents[i] = new DungeonSquare(sq.xCoord + directions[i][0], sq.yCoord + directions[i][1]); //make a new DungeonSquare at these coordinates
                DungeonSquare adj = sq.adjacents[i]; //set the square to this adjacency

                emptyCoordinatesToSquare.Add(adj.id, adj); //add the square to the list of empties
            }
        }
    }

    List<DungeonSquare> RefreshVacancies()
    {
        List<DungeonSquare> newList = new List<DungeonSquare>();

        foreach (DungeonSquare sq in emptyCoordinatesToSquare.Values)
            newList.Add(sq);

        return newList;
    }
}