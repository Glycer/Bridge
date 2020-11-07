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
    public bool queueReady;
    public bool comboExpired;
    HanzoAttack currAttack;

    // Start is called before the first frame update
    void Start()
    {
        currComboIndex = 0;
        currAttack = null;
        abilities = new Ability[4];
    }

    protected override void Whack(bool keyDown)
    {
        if (keyDown && (currComboIndex >= combo.Length || combo[currComboIndex] == null || comboExpired))
        {
            currAttack = null;
            currComboIndex = 0;
        }
        if (keyDown && queueReady)
        {
            if (currAttack != null)
                currAttack.StopTimer();
            currAttack = combo[currComboIndex];
            combo[currComboIndex].Swing();
            currComboIndex++;
        }
    }
    protected override void Secondary(bool keyDown)
    {
        if (keyDown && (currComboIndex >= alternateCombo.Length || alternateCombo[currComboIndex] == null || comboExpired))
        {
            currAttack = null;
            currComboIndex = 0;
        }
        if (keyDown && queueReady)
        {
            if (currAttack != null)
                currAttack.StopTimer();
            currAttack = alternateCombo[currComboIndex];
            alternateCombo[currComboIndex].Swing();
            currComboIndex++;
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
    }

    IEnumerator Dodge()
    {
        for (int i = 0; i < 5; i++)
        {
            motion.transform.Translate(0, 0, 0.6f);
            yield return new WaitForSeconds(0.01f);
        }
        dodging = false;
        // Brief vulnerability period if dodge is poorly timed
        yield return new WaitForSeconds(0.2f);
        motion.motionLocked = false;
    }
}
