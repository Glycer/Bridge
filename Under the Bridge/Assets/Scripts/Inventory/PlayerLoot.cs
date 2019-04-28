using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class PlayerLoot
{
    public static UnityAction<Loot, int> UpdateLoot;

    public enum Loot { Light, Dark, Life, Death, Fire, Water };

    public static Dictionary<Loot, int> loots = new Dictionary<Loot, int>() {
        { Loot.Light, 0 },
        { Loot.Dark, 0 },
        { Loot.Life, 0 },
        { Loot.Death, 0 },
        { Loot.Fire, 0 },
        { Loot.Water, 0 }
    };
}
