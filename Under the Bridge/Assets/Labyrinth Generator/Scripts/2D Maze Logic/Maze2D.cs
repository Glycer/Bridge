using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze2D : MonoBehaviour
{
    public GameObject mazeSquare;

    public int width = 1;
    public int length = 1;

    public float gapFrequency;
    public bool useColumns = true;

    Vector3[,] coordinates;
    Maze2DSquare[,] squares;

    LevelPrefabs levelPrefabs;

    public Maze2DSquare entranceSq;
    public Maze2DSquare exitSq;

    private void Awake()
    {
        levelPrefabs = GetComponent<LevelPrefabs>();
    }

    public void GenerateGrid()
    {
        Maze2DLogic.ClearGrid(transform);
        squares = new Maze2DSquare[length, width];
        PrefabDimensions dimensions = mazeSquare.GetComponent<PrefabDimensions>();
        coordinates = Maze2DLogic.GenerateCoordinates(dimensions.width, dimensions.length, squares);

        //[Layer][Column][Cluster][Square]
        List<List<Maze2DSquare[]>> gridClusters;
        Maze2DLogic.WriteClusters(gapFrequency, out gridClusters, squares);

        Maze2DLogic.GenerateSquares(transform, useColumns, levelPrefabs, mazeSquare, coordinates, squares);
        GenerateOutlets();
    }

    void GenerateOutlets()
    {
        Maze2DLogic.SetOutlet(useColumns, levelPrefabs, levelPrefabs.entrance, out entranceSq, squares);
        Maze2DLogic.SetOutlet(useColumns, levelPrefabs, levelPrefabs.exit, out exitSq, squares);
    }
}
