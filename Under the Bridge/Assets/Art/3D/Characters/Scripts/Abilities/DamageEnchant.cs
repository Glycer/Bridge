﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnchant : Enchantment
{
    public override void UseEnchantment(MonsterStats enemy = null)
    {
        if (enemy != null)
            enemy.TakeDamage(2);
    }
}
