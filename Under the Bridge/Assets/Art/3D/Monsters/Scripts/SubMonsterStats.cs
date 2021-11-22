﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for subsections of a monster, relays information to source monster stats
public class SubMonsterStats : MonsterStats
{
    public MonsterStats parentStats;

    // TODO: add defense modifier
    public override bool TakeDamage(int damage)
    {
        return parentStats.TakeDamage(damage);
    }
}