using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each dungeon needs 2 cameras. When the player is outside the dungeon, the cameras project to the entrances.
/// When the player is inside the dungeon, the cameras project to the current level from the levels/outlet on either end.
/// </summary>
public class PortalCams : MonoBehaviour
{
    public MazeManager mazeManager;

    public List<Camera> cams;

    Dictionary<Camera, Transform> camPortals;

    // Start is called before the first frame update
    void Start()
    {
        camPortals = new Dictionary<Camera, Transform>();
        for (int i = 0; i < cams.Count; i++)
            camPortals.Add(cams[i], transform);

        SetCams(null);
    }

    public void SetCams(Maze level)
    {
        if (level == null)
        {
            Reposition(mazeManager.entrance, mazeManager.exit);
            return;
        }

        Portal enter = level.entranceSq.layout.GetComponentInChildren<Portal>();
        Portal exit = level.exitSq.layout.GetComponentInChildren<Portal>();

        Reposition(enter, exit);
    }

    public void Reposition(Portal _entrance, Portal _exit)
    {
        Portal[] enterExit = new Portal[] { _entrance, _exit };

        for (int i = 0; i < cams.Count; i++)
            camPortals[cams[i]] = enterExit[i].destination.transform;

        AlignCams();
    }

    void AlignCams()
    {
        for (int i = 0; i < cams.Count; i++)
        {
            cams[i].transform.position = camPortals[cams[i]].position;
            cams[i].transform.rotation = camPortals[cams[i]].rotation;
        }
    }
}
