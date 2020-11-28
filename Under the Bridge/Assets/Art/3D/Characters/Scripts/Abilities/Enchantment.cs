using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enchantment : Ability
{
    public Text chargesDisplay;
    public int maxCharges;
    protected int charges;

    public void SetEnchant()
    {
        PlayerStats.SpendMana(manaCost);
        charges = maxCharges;
        chargesDisplay.text = charges.ToString();
    }

    public virtual void UseEnchantment(MonsterStats enemy = null)
    {
    }
    public bool ConsumeCharge()
    {
        charges--;
        if (charges > 0)
            chargesDisplay.text = charges.ToString();
        else
            chargesDisplay.text = "";
        return charges <= 0;
    }
    public bool GetCharge()
    {
        if (charges > 0)
            return true;
        else
            return false;
    }

    public override void UseAbility(bool keyDown)
    {
        if (keyDown)
        {
            SetEnchant();
            weapon.currEnchant = this;
        }
    }
}
