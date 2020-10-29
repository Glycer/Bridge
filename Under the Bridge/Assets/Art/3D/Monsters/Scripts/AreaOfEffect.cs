using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    PlayerStats player;
    public int strength;

    void OnTriggerEnter(Collider col)
    {
        player = col.gameObject.GetComponent<PlayerStats>();
    }
    void OnTriggerExit(Collider col)
    {
        player = null;
    }

    public void Attack()
    {
        if (player != null)
            player.TakeDamage(strength);
    }
}
