using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MischiefStats : MonsterStats
{
    void OnEnable()
    {
        hitPoints = 3;
        moveSpeed = 1;
        turnSpeed = 1;
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        PlayerLoot.UpdateLoot(PlayerLoot.Loot.Light, 5);
    }
}
