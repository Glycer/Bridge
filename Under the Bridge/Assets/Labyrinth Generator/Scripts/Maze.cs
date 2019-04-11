using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject mazeSquare;
    public MonsterSpawns spawns;

    public int LevelNum { get; set; }

    public int width = 1;
    public int length = 1;
    public int height = 1;

    public float gapFrequency;
    public bool useColumns = true;

    Vector3[,,] coordinates;
    MazeSquare[,,] squares;

    LevelPrefabs levelPrefabs;

    public MazeSquare entranceSq;
    public MazeSquare exitSq;

    private void Awake()
    {
        levelPrefabs = GetComponent<LevelPrefabs>();
    }

    public void GenerateGrid()
    {
        MazeGeneralLogic.ClearGrid(transform);
        squares = new MazeSquare[length, width, height];
        PrefabDimensions dimensions = mazeSquare.GetComponent<PrefabDimensions>();
        coordinates = MazeGeneralLogic.GenerateCoordinates(dimensions.width, dimensions.length, dimensions.height, squares);

        //[Layer][Column][Cluster][Square]
        List<List<List<MazeSquare[]>>> gridClusters;
        MazeClusterLogic.WriteClusters(gapFrequency, out gridClusters, squares);

        MazeGeneralLogic.GenerateSquares(transform, useColumns, levelPrefabs, mazeSquare, coordinates, squares);
        GenerateOutlets();

        SpawnMonsters(spawns.monsters);
    }

    void GenerateOutlets()
    {
        MazeGeneralLogic.SetOutlet(useColumns, levelPrefabs, levelPrefabs.entrance, out entranceSq, squares);
        MazeGeneralLogic.SetOutlet(useColumns, levelPrefabs, levelPrefabs.exit, out exitSq, squares);
    }

    public void SpawnMonsters(List<GameObject> monsters)
    {
        MazeSquare destination;
        Vector3 coords;

        for (int i = 0; i < monsters.Count; i++)
        {
            do
                destination = (squares[Random.Range(0, length), Random.Range(0, width), Random.Range(0, height)]);
            while (destination == null);

            coords = destination.layout.transform.position;
            monsters[i].GetComponent<MonsterStats>().Spawn(coords);
        }
    }
}