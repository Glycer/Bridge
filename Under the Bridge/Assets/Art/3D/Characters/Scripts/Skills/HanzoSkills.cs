using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoSkills : PlayerSkills
{
    Coroutine dodge;

    public HanzoWeapon[] weapons;
    int currComboIndex;
    // bool indicates whether any attacks are active
    bool awaitingAttack;
    public bool comboExpired;
    HanzoAttack currAttack;

    // TODO: Temp code used until runtime ability setting is implemented
    public Ability vampireBite;

    // Start is called before the first frame update
    void Awake()
    {
        awaitingAttack = true;
        dodging = false;
        currComboIndex = 0;
        currAttack = null;
        currActiveWeaponIndex = 0;
        secondaries = weapons[currActiveWeaponIndex].GetSecondaries();
    }

    protected override void Attack(bool keyDown)
    {
        if (keyDown)
            Attack(weapons[currActiveWeaponIndex].GetCombo());
    }
    void Attack(HanzoAttack[] currCombo)
    {
        if (!dodging)
        {
            if (currComboIndex >= currCombo.Length || currCombo[currComboIndex] == null || comboExpired)
                currComboIndex = 0;
            if (currCombo[currComboIndex] != null && currAttack != null && currAttack.queueReady)
            {
                currAttack.QueueSwing(currCombo[currComboIndex]);
                currAttack = currCombo[currComboIndex];
                currComboIndex++;
            }
            if (awaitingAttack)
            {
                if (currAttack != null)
                    currAttack.StopTimer();
                currAttack = currCombo[currComboIndex];
                currCombo[currComboIndex].Swing();
                currComboIndex++;
            }
        }
    }
    protected override void Secondary(int secondaryIndex)
    {
        if ((!dodging || awaitingAttack) && secondaries.Length > secondaryIndex)
        {
            if (currAttack != null && currAttack.queueReady)
            {
                // Reset combo
                currComboIndex = 0;
                currAttack.QueueSwing(secondaries[secondaryIndex].GetHanzoAttack());
                currAttack = secondaries[secondaryIndex].GetHanzoAttack();
            }
            else if (awaitingAttack)
            {
                currComboIndex = 0;
                if (currAttack != null)
                    currAttack.StopTimer();
                currAttack = secondaries[secondaryIndex].GetHanzoAttack();
                secondaries[secondaryIndex].GetHanzoAttack().Swing();
            }
        }
    }
    protected override void Defense(bool keyDown)
    {
        if (keyDown && !PlayerMotion.MotionLocked())
        {
            dodging = true;
            LockPlayerMotion(0.25f);
            dodge = StartCoroutine(Dodge());
        }
    }

    protected override void SwitchWeapon(int weaponIndex)
    {
        // Switch weapon when attack is finished
        if (awaitingAttack)
        {
            weapons[currActiveWeaponIndex].gameObject.SetActive(false);
            currActiveWeaponIndex = weaponIndex;
            weapons[currActiveWeaponIndex].gameObject.SetActive(true);
            secondaries = weapons[currActiveWeaponIndex].GetSecondaries();
        }
    }

    private void OnDisable()
    {
        dodging = false;
        dodge = null;
        awaitingAttack = true;
        comboExpired = false;
        currComboIndex = 0;
        currAttack = null;
        if (HUD != null)
            HUD.SetActive(false);
    }

    public void SetAwaiting(bool isAwaiting)
    {
        awaitingAttack = isAwaiting;
        weapons[currActiveWeaponIndex].SetWeaponActive(!isAwaiting);
    }

    IEnumerator Dodge()
    {
        Vector3 direction = charMotion.transform.TransformVector(new Vector3(0, 0, 1));
        direction *= 0.6f;
        for (int i = 0; i < 5; i++)
        {
            motion.transform.Translate(direction, Space.World);
            yield return new WaitForSeconds(0.01f);
        }
        dodging = false;
        // Brief vulnerability period if dodge is poorly timed
        yield return new WaitForSeconds(0.2f);
        PlayerMotion.LockMotion(false);
    }
}
