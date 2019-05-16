using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprawlMaze : MonoBehaviour
{
    public GameObject sq;

    public GameObject entrance;
    public GameObject exit;
    SprawlerSquare[] outlets;

    public bool isFlat;

    public int size;
    public Vector3 spread;

    public float turnFrequency;
    public float deadEndFrequency;

    LevelPrefabs prefabs;

    GameObject mazeInstance;
    List<SprawlerSquare> squares = new List<SprawlerSquare>();

    // Start is called before the first frame update
    void Start()
    {
        prefabs = GetComponent<LevelPrefabs>();
        outlets = new SprawlerSquare[2];

        ReloadMaze();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ReloadMaze();
    }

    void ReloadMaze()
    {
        if (mazeInstance != null)
        {
            Destroy(mazeInstance);
            squares.Clear();
        }

        mazeInstance = new GameObject("Instance");
        mazeInstance.transform.parent = transform;
        mazeInstance.transform.localPosition = Vector3.zero;

        squares = SprawlGeneratorLogic.Generate(outlets, isFlat, size, spread, turnFrequency, deadEndFrequency);

        for (int i = 0; i < squares.Count - 2; i++)
        {
            SprawlerSquare square = squares[i];

            square.layout = SprawlGeneratorLogic.BuildSquare(square, prefabs, sq);

            square.layout.transform.parent = mazeInstance.transform;
            square.layout.transform.localPosition = SprawlGeneratorLogic.SquarePosition(square, sq.GetComponent<PrefabDimensions>());

            square.layout.name = "Sq" + square.coordinates.ToString();
        }

        for (int i = squares.Count - 2; i < squares.Count; i++)
        {
            squares[i].layout = Instantiate(i == squares.Count-2 ? entrance : exit, mazeInstance.transform);
            
            squares[i].layout.transform.localPosition = SprawlGeneratorLogic.SquarePosition(squares[i], sq.GetComponent<PrefabDimensions>());
        }
    }
}
