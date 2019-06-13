using System.Collections.Generic;
using UnityEngine;

public class TownObjects : MonoBehaviour {

    public enum ObjectType { Building, Tree, Doodad };

    Dictionary<ObjectType, List<GameObject>> mainLists;
    Dictionary<ObjectType, List<GameObject>> ghostLists;

    Placement placement;

    public List<GameObject> buildings;
    public List<GameObject> buildingGhosts;

    public List<GameObject> trees;
    public List<GameObject> treeGhosts;

    public List<GameObject> doodads;
    public List<GameObject> doodadGhosts;

    private void Start()
    {
        placement = GetComponent<Placement>();

        mainLists = new Dictionary<ObjectType, List<GameObject>> {
            { ObjectType.Building, buildings },
            { ObjectType.Tree, trees },
            { ObjectType.Doodad, doodads }
        };

        ghostLists = new Dictionary<ObjectType, List<GameObject>> {
            { ObjectType.Building, buildingGhosts },
            { ObjectType.Tree, treeGhosts },
            { ObjectType.Doodad, doodadGhosts }
        };
    }

    /*public GameObject GetGhost(GameObject realObject)
    {
        // get the ghost appropriate
        return null;
    }*/

    public void AddBuilding(GameObject building)
    {
        buildings.Add(building);
    }

    public void SetPlacee(ObjectType type, int index)
    {
        placement.placee = mainLists[type][index];
        placement.placeeGhost = ghostLists[type][index];
    }
}
