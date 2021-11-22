using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for disconnected monster area attacks
public class AreaAttack : MonoBehaviour
{
    public int strength;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerMotion>())
            PlayerStats.TakeDamage(strength);
    }
}
