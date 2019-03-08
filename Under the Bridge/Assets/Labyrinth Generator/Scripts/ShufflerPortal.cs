using UnityEngine;

public class ShufflerPortal : MonoBehaviour
{
    Portal portal;
    MazeManager manager;
    Maze maze;

    private void Start()
    {
        portal = GetComponent<Portal>();
        manager = GetComponentInParent<MazeManager>();
        maze = GetComponentInParent<Maze>();

        portal.Port += Shuffle;
    }

    void Shuffle(Collider dummy)
    {
        manager.ReloadLevel(maze.LevelNum);
    }
}