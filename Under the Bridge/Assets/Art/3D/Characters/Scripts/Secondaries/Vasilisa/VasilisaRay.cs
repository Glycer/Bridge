using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaRay : VasilisaAttack
{
    // Parameters
    public float damageFalloff;
    public int abilityStrength;
    public int manaGain;
    public float attackTick;

    public bool semiAuto;
    const float SEMI_AUTO_APPLICATION_DELAY = 0.02f;

    Coroutine applyAttack;
    public TargetCollider rayCollider;

    const float ORIENT_DELAY = 0.02f;

    public override void UseAttack(bool keyDown)
    {
        if (semiAuto)
        {
            if (keyDown)
            {
                applyAttack = StartCoroutine(ActivateAttack());
            }
        }
        else
        {
            // Activate attack
            if (keyDown)
            {
                rayCollider.gameObject.SetActive(true);
                applyAttack = StartCoroutine(ApplyAttack());
            }
            // Deactivate attack
            else
            {
                rayCollider.gameObject.SetActive(false);
                if (applyAttack != null)
                    StopCoroutine(applyAttack);
            }
        }
    }

    IEnumerator ApplyAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackTick);
            foreach (Collider enemy in rayCollider.targets)
            {
                if (enemy.GetComponent<MonsterStats>() != null)
                {
                    enemy.GetComponent<MonsterStats>().TakeDamage(abilityStrength);
                    PlayerStats.AddMana(1, manaGain);
                }
            }
        }
    }

    IEnumerator ActivateAttack()
    {
        rayCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(SEMI_AUTO_APPLICATION_DELAY);
        foreach (Collider enemy in rayCollider.targets)
        {
            if (enemy.GetComponent<MonsterStats>() != null)
            {
                enemy.GetComponent<MonsterStats>().TakeDamage(abilityStrength);
                PlayerStats.AddMana(1, manaGain);
            }
        }
        yield return new WaitForSeconds(lockTime - SEMI_AUTO_APPLICATION_DELAY);
        rayCollider.gameObject.SetActive(false);
    }
}
