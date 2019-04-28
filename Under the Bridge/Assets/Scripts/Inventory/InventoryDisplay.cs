using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Text lightLoot;
    public Text darkLoot;
    public Text lifeLoot;
    public Text deathLoot;
    public Text fireLoot;
    public Text waterLoot;

    Dictionary<PlayerLoot.Loot, Text> lootSet;

    private void Start()
    {
        PlayerLoot.UpdateLoot += UpdateLoot;

         lootSet = new Dictionary<PlayerLoot.Loot, Text>() {
             { PlayerLoot.Loot.Light, lightLoot },
             { PlayerLoot.Loot.Dark, darkLoot },
             { PlayerLoot.Loot.Life, lifeLoot },
             { PlayerLoot.Loot.Death, deathLoot },
             { PlayerLoot.Loot.Fire, fireLoot },
             { PlayerLoot.Loot.Water, waterLoot }
         };
    }

    void UpdateLoot(PlayerLoot.Loot loot, int num)
    {
        PlayerLoot.loots[loot] += num;
        lootSet[loot].text = PlayerLoot.loots[loot].ToString();
    }
}
