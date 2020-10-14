using UnityEngine;

public class Maze2DManager : MonoBehaviour
{
    public GameObject[] levels;

    public GameObject entrance;
    public GameObject exit;

    Portal enPort;
    Portal exPort;

    private void Start()
    {
        enPort = entrance.GetComponentInChildren<Portal>();
        exPort = exit.GetComponentInChildren<Portal>();

        foreach (GameObject level in levels)
            if (level.GetComponent<Maze2D>() != null)
                level.GetComponent<Maze2D>().GenerateGrid();

        LinkPortals();
    }

    public void LinkPortals()
    {
        Portal previousP;
        Portal nextP;

        if (levels.Length == 1)
        {
            MazeGeneralLogic.LinkPortalPair(enPort, levels[0].GetComponent<Maze2D>().entranceSq.layout.GetComponentInChildren<Portal>());
            MazeGeneralLogic.LinkPortalPair(exPort, levels[0].GetComponent<Maze2D>().exitSq.layout.GetComponentInChildren<Portal>());
        }
        else
            for (int i = 0; i < levels.Length; i++)
            {
                Portal entranceP = levels[i].GetComponent<Maze2D>().entranceSq.layout.GetComponentInChildren<Portal>();
                Portal exitP = levels[i].GetComponent<Maze2D>().exitSq.layout.GetComponentInChildren<Portal>();

                if (i == 0)
                {
                    MazeGeneralLogic.LinkPortalPair(entranceP, enPort);
                    continue;
                }
                else if (i == levels.Length - 1)
                {
                    MazeGeneralLogic.LinkPortalPair(exitP, exPort);
                    break;
                }

                previousP = levels[i - 1].GetComponent<Maze2D>().exitSq.layout.GetComponentInChildren<Portal>();
                MazeGeneralLogic.LinkPortalPair(entranceP, previousP);

                nextP = levels[i + 1].GetComponent<Maze2D>().entranceSq.layout.GetComponentInChildren<Portal>();
                MazeGeneralLogic.LinkPortalPair(exitP, nextP);
            }
    }
}
