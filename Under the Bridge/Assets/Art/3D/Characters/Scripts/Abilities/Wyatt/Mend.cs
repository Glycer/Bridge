using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mend : Ability
{
    const int HEAL_AMOUNT = 20;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown && !PlayerStats.HealthFull())
        {
            PlayerStats.TakeDamage(-HEAL_AMOUNT, false);
            PlayerStats.SpendMana(manaCost);
        }
    }
}
