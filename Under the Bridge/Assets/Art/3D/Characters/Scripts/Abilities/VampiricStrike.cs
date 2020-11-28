using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VampiricStrike : Enchantment
{
    PlayerStats currStats;

    public override void UseEnchantment(MonsterStats enemy = null)
    {
        PlayerStats.TakeDamage(PlayerStats.playerStrength * -1);
    }
}
