using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoAttack : MonoBehaviour
{
    public int strength;
    public int manaFill;
    public float swingDuration;

    Coroutine timer;
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
    }

    public void Swing()
    {
        HanzoWeapon.currStrength = strength;
        HanzoWeapon.currManaFill = manaFill;

        skills.queueReady = false;
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
        yield return new WaitForSeconds(swingDuration);
        skills.queueReady = true;
        yield return new WaitForSeconds(1);
        skills.comboExpired = true;
    }
}
