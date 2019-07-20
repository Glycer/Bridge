using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    public Collider enemy;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Hit!");
        collider.gameObject.GetComponent<MonsterStats>().TakeDamage(1);
    }
}
