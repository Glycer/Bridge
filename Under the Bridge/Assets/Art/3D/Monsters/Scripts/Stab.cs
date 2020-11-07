using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    public PlayerFound player;
    public MonsterStats stats;

    void OnTriggerEnter(Collider col)
    {
        PlayerStats.TakeDamage(stats.strength);
        Debug.Log("You have been hit.");
    }
}
