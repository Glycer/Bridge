using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollider : MonoBehaviour
{
    public List<Collider> targets = new List<Collider>();

    void OnTriggerEnter(Collider col)
    {
        targets.Add(col);
    }

    void OnTriggerExit(Collider col)
    {
        targets.Remove(col);
    }
}
