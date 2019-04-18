using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MischiefStats : MonsterStats
{
    void OnEnable()
    {
        hitPoints = 3;
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        PlayerLoot.AddDeath(5);
    }
}
