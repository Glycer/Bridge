using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSquare : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	}

    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
}
