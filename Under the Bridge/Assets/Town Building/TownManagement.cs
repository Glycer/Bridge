using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManagement : MonoBehaviour
{
    public Camera playerCam;
    public Camera townCam;
    bool inTownView;

    void Start()
    {
        inTownView = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Inputs.townView))
            TownView();
    }

    void TownView()
    {
        inTownView = (inTownView ? false : true);
        playerCam.enabled = !inTownView;
        townCam.enabled = inTownView;
    }
}
