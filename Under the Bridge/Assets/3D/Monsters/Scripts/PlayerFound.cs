using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFound : MonoBehaviour
{
    public bool foundTarget;

    // Start is called before the first frame update
    void Start()
    {
        foundTarget = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        foundTarget = true;
    }

    void OnTriggerExit(Collider col)
    {
        foundTarget = false;
    }
}
