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
        dodging = false;
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
            Attack(combo);
    }
    protected override void Secondary(bool keyDown)
    {
        if (keyDown)
            Attack(alternateCombo);
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

    protected override void Defense(bool keyDown)
    {
        if (keyDown && !motion.motionLocked)
        {
            dodging = true;
            LockPlayerMotion(0.25f);
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
        // Unlocks motion
        ReleaseMotionLock();
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
        motion.motionLocked = false;
    }
}
