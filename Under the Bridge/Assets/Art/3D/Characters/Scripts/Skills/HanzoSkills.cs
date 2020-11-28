using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoSkills : PlayerSkills
{
    Coroutine dodge;

    // TODO: Combos should likely be made private once runtime customization is implemented
    public HanzoAttack[] combo;
    public HanzoAttack[] alternateCombo;
    int currComboIndex;
    public bool awaitingAttack;
    public bool comboExpired;
    HanzoAttack currAttack;

    // TODO: Temp code used until runtime ability setting is implemented
    public Ability vampiricStrike;
    public Ability vampireBite;

    // Start is called before the first frame update
    void Awake()
    {
        currComboIndex = 0;
        currAttack = null;
        currActiveWeaponIndex = 0;
        abilities = new Ability[4];
        abilities[0] = vampiricStrike;
        abilities[1] = vampireBite;
    }

    protected override void Whack(bool keyDown)
    {
        if (keyDown)
        {
            if (currComboIndex >= combo.Length || combo[currComboIndex] == null || comboExpired)
                currComboIndex = 0;
            if (combo[currComboIndex] != null && currAttack != null && currAttack.queueReady)
            {
                currAttack.QueueSwing(combo[currComboIndex]);
                currAttack = combo[currComboIndex];
                currComboIndex++;
            }
            if (awaitingAttack)
            {
                if (currAttack != null)
                    currAttack.StopTimer();
                currAttack = combo[currComboIndex];
                combo[currComboIndex].Swing();
                currComboIndex++;
            }
        }
    }
    protected override void Secondary(bool keyDown)
    {
        if (keyDown)
        {
            if (currComboIndex >= alternateCombo.Length || alternateCombo[currComboIndex] == null || comboExpired)
                currComboIndex = 0;
            if (alternateCombo[currComboIndex] != null && currAttack != null && currAttack.queueReady)
            {
                currAttack.QueueSwing(alternateCombo[currComboIndex]);
                currAttack = alternateCombo[currComboIndex];
                currComboIndex++;
            }
            if (awaitingAttack)
            {
                if (currAttack != null)
                    currAttack.StopTimer();
                currAttack = alternateCombo[currComboIndex];
                alternateCombo[currComboIndex].Swing();
                currComboIndex++;
            }
        }
    }
    protected override void Defense(bool keyDown)
    {
        if (keyDown && motion.motionLocked == false)
        {
            dodging = true;
            motion.motionLocked = true;
            dodge = StartCoroutine(Dodge());
        }
    }

    private void OnDisable()
    {
        dodging = false;
        motion.motionLocked = false;
        dodge = null;
        awaitingAttack = true;
        comboExpired = false;
        currComboIndex = 0;
        currAttack = null;
        if (HUD != null)
            HUD.SetActive(false);
    }

    IEnumerator Dodge()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        if (Input.GetAxis(Inputs.playerStrafeAxis) < 0)
            direction += new Vector3(-0.6f, 0, 0);
        else if (Input.GetAxis(Inputs.playerStrafeAxis) > 0)
            direction += new Vector3(0.6f, 0, 0);
        else if (Input.GetAxis(Inputs.playerVAxis) < 0)
            direction += new Vector3(0, 0, -0.6f);
        else
            direction += new Vector3(0, 0, 0.6f);


        for (int i = 0; i < 5; i++)
        {
            motion.transform.Translate(direction);
            yield return new WaitForSeconds(0.01f);
        }
        dodging = false;
        // Brief vulnerability period if dodge is poorly timed
        yield return new WaitForSeconds(0.2f);
        motion.motionLocked = false;
    }
}
