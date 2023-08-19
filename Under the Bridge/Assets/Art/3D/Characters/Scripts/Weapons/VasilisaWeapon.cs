using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaWeapon : Weapon
{
    public bool attackLocked;

    public int currSpellIndex;

    void OnEnable()
    {
        attackLocked = false;

        currSpellIndex = 0;
    }

    // Set spell
    public override void Secondary(int newIndex)
    {
        if (newIndex < secondaries.Length)
            currSpellIndex = newIndex;
    }

    public override void Attack(bool keyDown)
    {
        if (!attackLocked)
        {
            secondaries[currSpellIndex].UseAttack(keyDown);
            StartCoroutine(LockAttack(secondaries[currSpellIndex].GetLockTime()));
        }
    }
    protected IEnumerator LockAttack(float LockTime)
    {
        attackLocked = true;
        yield return new WaitForSeconds(LockTime);
        attackLocked = false;
    }

    public bool AttackLocked()
    {
        return attackLocked;
    }
}
