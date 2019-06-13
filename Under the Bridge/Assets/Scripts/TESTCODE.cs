using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTCODE : MonoBehaviour
{
    public Buildings buildings;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            buildings.addBlueprint("House");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildings.addBlueprint("Big house");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            buildings.addBlueprint("Factory");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            buildings.addBlueprint("Training center");
        }
    }
}
