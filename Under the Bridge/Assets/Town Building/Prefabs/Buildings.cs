using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buildings : MonoBehaviour
{
    Button newButton;
    public Button button;
    public Text text;
    static List<string> buildingBlueprints;
    static Dictionary<string, int> storedBuildings;
    static Dictionary<string, GameObject> buildings;
    static Dictionary<string, GameObject> buildingGhosts;

    void Start()
    {
        buildingBlueprints = new List<string>();
        storedBuildings = new Dictionary<string, int>();
        buildings = new Dictionary<string, GameObject>();
        buildingGhosts = new Dictionary<string, GameObject>();
    }

    // Adds blueprint to list of blueprints
    public void AddBlueprint(string newBlueprint, GameObject realObject, GameObject ghost)
    {
        // Redundancy avoidance
        foreach (string blueprint in buildingBlueprints)
        {
            if (newBlueprint == blueprint)
            {
                return;
            }
        }
        // Stores prefabs
        buildings.Add(newBlueprint, realObject);
        buildingGhosts.Add(newBlueprint, ghost);

        AddBuildingButton(newBlueprint);

        buildingBlueprints.Add(newBlueprint);

        storedBuildings.Add(newBlueprint, 0);

        buildingBlueprints.Sort();
    }

    public GameObject GetBuilding(string realObject)
    {
        return buildings[realObject];
    }

    public GameObject GetGhost(string ghostObject)
    {
        return buildingGhosts[ghostObject];
    }

    void AddBuilding(string newBuilding)
    {
        storedBuildings[newBuilding] = storedBuildings[newBuilding] + 1;
    }

    void AddBuildingButton(string newBuilding)
    {
        text.text = newBuilding;
        if (buildingBlueprints.Count == 0)
        {
            newButton = Instantiate(button, transform);
            button.transform.position = new Vector3(button.transform.position.x + 100, button.transform.position.y, 0);

        }
        else if (buildingBlueprints.Count % 3 == 2)
        {
            newButton = Instantiate(button, transform);
            button.transform.position = new Vector3(button.transform.position.x - 200, button.transform.position.y - 100, 0);
        }
        else
        {
            newButton = Instantiate(button, transform);
            button.transform.position = new Vector3(button.transform.position.x + 100, button.transform.position.y, 0);
        }
        newButton.gameObject.SetActive(true);
    }
}
