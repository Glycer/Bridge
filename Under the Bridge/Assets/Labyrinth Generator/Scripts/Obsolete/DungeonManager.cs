using UnityEngine;

public class DungeonManager : MonoBehaviour {

    public GameObject[] levels;

    public Portal entrance;
    public Portal exit;

    private void Start()
    {
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
            MazeGeneralLogic.LinkPortalPair(entrance, levels[0].GetComponent<Maze>().entranceSq.layout.GetComponentInChildren<Portal>());
            MazeGeneralLogic.LinkPortalPair(exit, levels[0].GetComponent<Maze>().exitSq.layout.GetComponentInChildren<Portal>());
        }
        else
            for (int i = 0; i < levels.Length; i++) {
                Portal entranceP = levels[i].GetComponent<Maze>().entranceSq.layout.GetComponentInChildren<Portal>();
                Portal exitP = levels[i].GetComponent<Maze>().exitSq.layout.GetComponentInChildren<Portal>();

                if (i == 0) {
                    MazeGeneralLogic.LinkPortalPair(entranceP, entrance);
                    continue;
                }
                else if (i == levels.Length - 1) {
                    MazeGeneralLogic.LinkPortalPair(exitP, exit);
                    break;
                }

                previousP = levels[i - 1].GetComponent<Maze>().exitSq.layout.GetComponentInChildren<Portal>();
                MazeGeneralLogic.LinkPortalPair(entranceP, previousP);

                nextP = levels[i + 1].GetComponent<Maze>().entranceSq.layout.GetComponentInChildren<Portal>();
                MazeGeneralLogic.LinkPortalPair(exitP, nextP);
            }
    }
}
