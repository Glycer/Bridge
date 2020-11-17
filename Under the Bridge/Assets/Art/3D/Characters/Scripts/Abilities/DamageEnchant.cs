using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnchant : Enchantment
{
    public Text chargesDisplay;

    public override void SetEnchant()
    {
        PlayerStats.SpendMana(manaCost);
        charges = maxCharges;
        chargesDisplay.text = charges.ToString();
    }

    public override bool UseEnchantment(MonsterStats enemy = null)
    {
        charges--;
        if (enemy != null)
            enemy.TakeDamage(2);
        if (charges > 0)
            chargesDisplay.text = charges.ToString();
        else
            chargesDisplay.text = "";
        return charges <= 0;
    }
}
