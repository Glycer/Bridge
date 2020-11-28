using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollider : MonoBehaviour
{
    public List<Collider> targets = new List<Collider>();
    
    public void RefreshList()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null || !targets[i].gameObject.activeSelf)
            {
                targets.Remove(targets[i]);
                i = 0;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        targets.Add(col);
    }

    void OnTriggerExit(Collider col)
    {
        targets.Remove(col);
    }

    void OnDisable()
    {
        targets.Clear();
    }
}
