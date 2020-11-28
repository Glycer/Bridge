using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireBite : Ability
{
    public TargetCollider target;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown)
        {
            target.RefreshList();
            if (target.targets.Count > 0)
            {
                target.targets[0].GetComponent<MonsterStats>().TakeDamage(20);
                PlayerStats.TakeDamage(-20);
                PlayerStats.SpendMana(manaCost);
            }
        }
    }
}
