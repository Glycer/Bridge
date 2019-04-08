﻿using UnityEngine;

public class MazeManager : MonoBehaviour
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

        for (int i = 0; i < levels.Length; i++)
        {
            Maze maze;

            if (levels[i].GetComponentInChildren<Maze>() != null)
            {
                maze = levels[i].GetComponentInChildren<Maze>();
                maze.GenerateGrid();
                maze.LevelNum = i;
            }
        }

        LinkPortals();
    }

    public void LinkPortals()
    {
        Portal previousP;
        Portal nextP;

        if (levels.Length == 1)
        {
            LinkPair(enPort, levels[0].GetComponentInChildren<Maze>().entranceSq.layout.GetComponentInChildren<Portal>());
            LinkPair(exPort, levels[0].GetComponentInChildren<Maze>().exitSq.layout.GetComponentInChildren<Portal>());
        }
        else
            for (int i = 0; i < levels.Length; i++)
            {
                Portal entranceP = levels[i].GetComponentInChildren<Maze>().entranceSq.layout.GetComponentInChildren<Portal>();
                Portal exitP = levels[i].GetComponentInChildren<Maze>().exitSq.layout.GetComponentInChildren<Portal>();

                if (i == 0)
                {
                    LinkPair(entranceP, enPort);
                    continue;
                }
                else if (i == levels.Length - 1)
                {
                    LinkPair(exitP, exPort);
                    break;
                }

                previousP = levels[i - 1].GetComponentInChildren<Maze>().exitSq.layout.GetComponentInChildren<Portal>();
                LinkPair(entranceP, previousP);

                nextP = levels[i + 1].GetComponentInChildren<Maze>().entranceSq.layout.GetComponentInChildren<Portal>();
                LinkPair(exitP, nextP);
            }
    }

    void LinkPair(Portal portal1, Portal portal2)
    {
        portal1.destination = portal2.transform;
        portal2.destination = portal1.transform;
    }

    public void ReloadLevel(int levelNum)
    {
        Maze maze = levels[levelNum].GetComponentInChildren<Maze>();
        Portal entranceP;
        Portal exitP;

        maze.GenerateGrid();

        if (maze.entranceSq.layout != null && maze.exitSq.layout != null)
        {
            entranceP = maze.entranceSq.layout.GetComponentInChildren<Portal>();
            exitP = maze.exitSq.layout.GetComponentInChildren<Portal>();

            if (levelNum == 0)
            {
                LinkPair(entranceP, enPort);
                LinkPair(exitP, levels[levelNum + 1].GetComponentInChildren<Maze>().entranceSq.layout.GetComponentInChildren<Portal>());
            }
            else if (levelNum == levels.Length - 1)
            {
                LinkPair(exitP, exPort);
                LinkPair(entranceP, levels[levelNum - 1].GetComponentInChildren<Maze>().exitSq.layout.GetComponentInChildren<Portal>());
            }
            else
            {
                LinkPair(entranceP, levels[levelNum - 1].GetComponentInChildren<Maze>().exitSq.layout.GetComponentInChildren<Portal>());
                LinkPair(exitP, levels[levelNum + 1].GetComponentInChildren<Maze>().entranceSq.layout.GetComponentInChildren<Portal>());
            }
        }
    }
}