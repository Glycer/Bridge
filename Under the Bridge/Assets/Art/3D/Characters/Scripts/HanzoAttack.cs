using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoAttack : MonoBehaviour
{
    public int strength;
    public int manaFill;
    public float swingDuration;
    public bool queueReady;

    Coroutine timer;
    HanzoAttack queuedAttack;
    const float WAIT_FOR_ATTACK = 0.3f;
    public HanzoSkills skills;

    // TODO: Likely temporary
    public Animation swingAnimation;
    public string clipName;
    public AnimationClip swingAnimationClip;

    // Start is called before the first frame update
    void Start()
    {
        swingAnimation.AddClip(swingAnimationClip, clipName);
        swingDuration = swingAnimationClip.length;
        queuedAttack = null;
    }

    public void Swing()
    {
        HanzoWeapon.currStrength = strength;
        HanzoWeapon.currManaFill = manaFill;

        skills.awaitingAttack = false;
        skills.comboExpired = false;
        if (timer != null)
            StopCoroutine(timer);
        timer = StartCoroutine(SwingTimer());
        swingAnimation.clip = swingAnimationClip;
        swingAnimation.Play();
    }
    public void StopTimer()
    {
        if (timer != null)
            StopCoroutine(timer);
    }
    IEnumerator SwingTimer()
    {
        yield return new WaitForSeconds(swingDuration - WAIT_FOR_ATTACK);
        queueReady = true;
        yield return new WaitForSeconds(WAIT_FOR_ATTACK);
        // Terminates timer if an attack is queued
        queueReady = false;
        if (queuedAttack != null)
        {
            queuedAttack.Swing();
            queuedAttack = null;
        }
        else
        {
            skills.awaitingAttack = true;

            yield return new WaitForSeconds(1);
            skills.comboExpired = true;
        }
    }

    public void QueueSwing(HanzoAttack toQueue)
    {
        queuedAttack = toQueue;
    }
}
