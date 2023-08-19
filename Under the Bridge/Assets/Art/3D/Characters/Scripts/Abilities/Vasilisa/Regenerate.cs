using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : Ability
{
    Coroutine heal;

    const int HEAL_POWER = 2;
    const float HEAL_TICK = 0.5f;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown)
        {
            if (heal != null)
                StopCoroutine(heal);
            heal = StartCoroutine(Heal());
            SwapCharacter.LockSwap(true);
        }
        else
        {
            HaltHeal();
        }
    }

    void HaltHeal()
    {
        if (heal != null)
            StopCoroutine(heal);
        SwapCharacter.LockSwap(false);
    }

    IEnumerator Heal()
    {
        while (PlayerStats.CheckMana(manaCost))
        {
            yield return new WaitForSeconds(HEAL_TICK);
            PlayerStats.TakeDamage(-HEAL_POWER);
            PlayerStats.SpendMana(manaCost);
        }
        HaltHeal();
    }
}
