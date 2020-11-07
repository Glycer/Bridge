using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    PlayerMotion player;
    public int strength;

    void OnTriggerEnter(Collider col)
    {
        player = col.gameObject.GetComponent<PlayerMotion>();
    }
    void OnTriggerExit(Collider col)
    {
        player = null;
    }

    public void Attack()
    {
        if (player != null)
            PlayerStats.TakeDamage(strength);
    }
}
