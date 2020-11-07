using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFound : MonoBehaviour
{
    public Collider target;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerMotion>() != null)
            target = col;
    }

    void OnTriggerExit(Collider col)
    {
        if (target != null && col == target)
        target = null;
    }
}
