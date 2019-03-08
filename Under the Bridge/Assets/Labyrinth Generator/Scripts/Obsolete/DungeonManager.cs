using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    public GameObject[] levels;

    public GameObject entrance;
    public GameObject exit;

    Portal enPort;
    Portal exPort;

    private void Start()
    {
        enPort = entrance.GetComponent<Portal>();
        exPort = exit.GetComponent<Portal>();

        foreach (GameObject level in levels)
            if (level.GetComponent<Maze>() != null)
                level.GetComponent<Maze>().GenerateGrid();

        LinkPortals();
    }

    public void LinkPortals()
    {
        Portal previousP;
        Portal nextP;

        if (levels.Length == 1) {
            LinkPair(enPort, levels[0].GetComponent<Maze>().entranceSq.layout.GetComponentInChildren<Portal>());
            LinkPair(exPort, levels[0].GetComponent<Maze>().exitSq.layout.GetComponentInChildren<Portal>());
        }
        else
            for (int i = 0; i < levels.Length; i++) {
                Portal entranceP = levels[i].GetComponent<Maze>().entranceSq.layout.GetComponentInChildren<Portal>();
                Portal exitP = levels[i].GetComponent<Maze>().exitSq.layout.GetComponentInChildren<Portal>();

                if (i == 0) {
                    LinkPair(entranceP, enPort);
                    continue;
                }
                else if (i == levels.Length - 1) {
                    LinkPair(exitP, exPort);
                    break;
                }

                previousP = levels[i - 1].GetComponent<Maze>().exitSq.layout.GetComponentInChildren<Portal>();
                LinkPair(entranceP, previousP);

                nextP = levels[i + 1].GetComponent<Maze>().entranceSq.layout.GetComponentInChildren<Portal>();
                LinkPair(exitP, nextP);
            }
    }

    void LinkPair(Portal portal1, Portal portal2)
    {
        portal1.destination = portal2.transform;
        portal2.destination = portal1.transform;
    }
}
