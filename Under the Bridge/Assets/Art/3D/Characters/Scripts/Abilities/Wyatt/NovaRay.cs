using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaRay : Ability
{
    public TargetCollider targetCollider;

    const int RAY_DAMAGE = 5;
    const float RAY_TIME = 3;
    const float ATTACK_TICK = 0.2f;

    public override void UseAbility(bool keyDown)
    {
        PlayerStats.SpendMana(manaCost);
        PlayerMotion.LockMotion(true);
        StartCoroutine(ActivateRay());
    }

    IEnumerator ActivateRay()
    {
        float timePassed = 0;
        targetCollider.gameObject.SetActive(true);
        while (timePassed < RAY_TIME)
        {
            yield return new WaitForSeconds(ATTACK_TICK);
            timePassed += ATTACK_TICK;
            targetCollider.RefreshList();
            foreach (Collider target in targetCollider.targets)
            {
                if (target.GetComponent<MonsterStats>())
                    target.GetComponent<MonsterStats>().TakeDamage(RAY_DAMAGE);
            }
        }
        targetCollider.gameObject.SetActive(false);
        PlayerMotion.LockMotion(false);
    }
}
