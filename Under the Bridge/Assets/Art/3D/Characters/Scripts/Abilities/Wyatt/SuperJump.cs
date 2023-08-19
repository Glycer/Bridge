using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : Ability
{
    public PlayerMotion playerMotion;

    const float JUMP_MODIFIER = 2f;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown)
        {
            if (playerMotion.Jump(JUMP_MODIFIER))
                PlayerStats.SpendMana(manaCost);
        }
    }
}
