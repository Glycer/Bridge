using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchantment : Ability
{
    public int maxCharges;
    protected int charges;

    public virtual void SetEnchant()
    {
        PlayerStats.SpendMana(manaCost);
        charges = maxCharges;
    }

    public virtual bool UseEnchantment(MonsterStats enemy = null)
    {
        charges--;
        return charges <= 0;
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
