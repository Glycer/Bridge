using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Hit!");
    }
}
