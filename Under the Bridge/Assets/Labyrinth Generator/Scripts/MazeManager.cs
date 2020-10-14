using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public Maze[] levels;
    public PortalCams portalCams;

    public Portal entrance;
    public Portal exit;

    private void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            Maze maze = levels[i];
            maze.GenerateGrid();
            maze.LevelNum = i;
        }

        LinkPortals();
        LinkCams();
    }

    public void LinkPortals()
    {
        Portal previousP;
        Portal nextP;

        if (levels.Length == 1)
        {
            MazeGeneralLogic.LinkPortalPair(entrance, levels[0].entranceSq.layout.GetComponentInChildren<Portal>());
            MazeGeneralLogic.LinkPortalPair(exit, levels[0].exitSq.layout.GetComponentInChildren<Portal>());
        }
        else
            for (int i = 0; i < levels.Length; i++)
            {
                Portal entranceP = levels[i].entranceSq.layout.GetComponentInChildren<Portal>();
                Portal exitP = levels[i].exitSq.layout.GetComponentInChildren<Portal>();

                //Portal linking
                if (i == 0)
                {
                    MazeGeneralLogic.LinkPortalPair(entranceP, entrance);
                    continue;
                }
                else if (i == levels.Length - 1)
                {
                    MazeGeneralLogic.LinkPortalPair(exitP, exit);
                    break;
                }

                previousP = levels[i - 1].exitSq.layout.GetComponentInChildren<Portal>();
                MazeGeneralLogic.LinkPortalPair(entranceP, previousP);

                nextP = levels[i + 1].entranceSq.layout.GetComponentInChildren<Portal>();
                MazeGeneralLogic.LinkPortalPair(exitP, nextP);
            }
    }

    public void LinkCams()
    {
        entrance.connectedLevel = levels[0];
        entrance.SetCams += portalCams.SetCams;

        exit.connectedLevel = levels[levels.Length - 1];
        exit.SetCams += portalCams.SetCams;

        for (int i = 0; i < levels.Length; i++)
            LoadCams(i);
    }

    public void ReloadLevel(int levelNum)
    {
        Maze maze = levels[levelNum];

        maze.GenerateGrid();

        if (maze.entranceSq.layout != null && maze.exitSq.layout != null)
        {
            Portal entranceP = maze.entranceSq.layout.GetComponentInChildren<Portal>();
            Portal exitP = maze.exitSq.layout.GetComponentInChildren<Portal>();

            if (levelNum == 0)
            {
                MazeGeneralLogic.LinkPortalPair(entranceP, entrance);
                MazeGeneralLogic.LinkPortalPair(exitP, levels[levelNum + 1].entranceSq.layout.GetComponentInChildren<Portal>());
            }
            else if (levelNum == levels.Length - 1)
            {
                MazeGeneralLogic.LinkPortalPair(exitP, exit);
                MazeGeneralLogic.LinkPortalPair(entranceP, levels[levelNum - 1].exitSq.layout.GetComponentInChildren<Portal>());
            }
            else
            {
                MazeGeneralLogic.LinkPortalPair(entranceP, levels[levelNum - 1].exitSq.layout.GetComponentInChildren<Portal>());
                MazeGeneralLogic.LinkPortalPair(exitP, levels[levelNum + 1].entranceSq.layout.GetComponentInChildren<Portal>());
            }

            LoadCams(levelNum);
        }
    }

    void LoadCams(int levelNum)
    {
        Maze maze = levels[levelNum];

        if (maze.entranceSq.layout != null && maze.exitSq.layout != null)
        {
            Portal entranceP = maze.entranceSq.layout.GetComponentInChildren<Portal>();
            Portal exitP = maze.exitSq.layout.GetComponentInChildren<Portal>();

            entranceP.SetCams += portalCams.SetCams;
            exitP.SetCams += portalCams.SetCams;

            entranceP.connectedLevel = levelNum == 0 ? null : levels[levelNum - 1];
            exitP.connectedLevel = levelNum == levels.Length - 1 ? null : levels[levelNum + 1];
        }
    }
}