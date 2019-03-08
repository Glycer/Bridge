using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {

    LabyrinthGenerator gen;

	// Use this for initialization
	void Start () {
        gen = new LabyrinthGenerator();
        gen.GenerateGrid();
	}
}
