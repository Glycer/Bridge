using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTCODE : MonoBehaviour
{
    public Buildings buildings;
    public GameObject house;
    public GameObject bigHouse;
    public GameObject factory;
    public GameObject trainingCenter;
    public GameObject houseGhost;
    public GameObject bigHouseGhost;
    public GameObject factoryGhost;
    public GameObject trainingCenterGhost;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            buildings.AddBlueprint("House", house, houseGhost);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildings.AddBlueprint("Big house", bigHouse, bigHouseGhost);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            buildings.AddBlueprint("Factory", factory, factoryGhost);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            buildings.AddBlueprint("Training center", trainingCenter, trainingCenterGhost);
        }
    }
}
