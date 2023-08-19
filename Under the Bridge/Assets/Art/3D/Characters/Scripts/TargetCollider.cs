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

    public void DealDamage(int damage)
    {
        foreach (Collider target in targets)
        {
            if (target.GetComponent<MonsterStats>())
                target.GetComponent<MonsterStats>().TakeDamage(damage);
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
