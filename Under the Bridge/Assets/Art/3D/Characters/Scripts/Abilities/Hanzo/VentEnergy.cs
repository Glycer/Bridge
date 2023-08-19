using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentEnergy : Ability
{
    public TargetCollider targetCollider;
    public GameObject visualBlast;

    const int MITIGATION_FACTOR = 4;
    const float LOCK_TIME = 0.8f;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown)
        {
            targetCollider.DealDamage((PlayerStats.GetMana(0) + PlayerStats.GetMana(1) + PlayerStats.GetMana(2)) / MITIGATION_FACTOR);
            StartCoroutine(VisualLockup());
            PlayerStats.EmptyMana();
        }
    }

    IEnumerator VisualLockup()
    {
        PlayerMotion.LockMotion(true);
        visualBlast.SetActive(true);
        yield return new WaitForSeconds(LOCK_TIME);
        PlayerMotion.LockMotion(false);
        visualBlast.SetActive(false);
    }
}
