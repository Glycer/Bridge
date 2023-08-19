using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaFocus : VasilisaWeapon
{
    public Transform playerCam;
    public int focusRange;
    public Transform abilityParent;

    Ray ray;
    RaycastHit rayHit;

    public void Awake()
    {
        foreach (SecondaryAttack attack in secondaries)
        {
            attack.transform.SetParent(abilityParent);
        }
    }

    public override void Attack(bool keyDown)
    {
        if (!attackLocked && keyDown)
        {
            if (playerCam.GetComponent<CamLockOn>().camControl.locked)
            {
                secondaries[currSpellIndex].GetComponent<VasilisaAttack>().SetAttackPosition(playerCam.GetComponent<CamLockOn>().targetLocation.position + new Vector3(0, secondaries[currSpellIndex].GetComponent<VasilisaAttack>().GetVerticalOffset(), 0));
                secondaries[currSpellIndex].UseAttack(keyDown);
                StartCoroutine(LockAttack(secondaries[currSpellIndex].GetLockTime()));
            }
            else if (Physics.Raycast(playerCam.position, playerCam.TransformDirection(Vector3.forward), out rayHit, focusRange, LayerMask.GetMask("Environment", "Dungeon", "Ground")))
            {
                secondaries[currSpellIndex].GetComponent<VasilisaAttack>().SetAttackPosition(rayHit.point + new Vector3(0, secondaries[currSpellIndex].GetComponent<VasilisaAttack>().GetVerticalOffset(), 0));
                secondaries[currSpellIndex].UseAttack(keyDown);
                StartCoroutine(LockAttack(secondaries[currSpellIndex].GetLockTime()));
            }
        }
        else if (!keyDown)
        {
            secondaries[currSpellIndex].UseAttack(keyDown);
        }
    }
}