using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFound : MonoBehaviour
{
    public Collider target;

    void OnTriggerEnter(Collider col)
    {
        target = col;
    }

    void OnTriggerExit(Collider col)
    {
        target = null;
    }
}
