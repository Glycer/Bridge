using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Ability
{
    public TargetCollider meteor;

    const int METEOR_DAMAGE = 50;

    const float Y_OFFSET = 20;
    const float Z_OFFSET = 5;

    const float DROP_TIME = 3;
    const float TIME_TICK = 0.05f;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown)
        {
            meteor.gameObject.SetActive(true);
            meteor.transform.localPosition = transform.localPosition + new Vector3(0, Y_OFFSET, Z_OFFSET);
            StartCoroutine(DropMeteor());
        }
    }

    IEnumerator DropMeteor()
    {
        PlayerMotion.LockMotion(true);
        float timePassed = 0;
        while (timePassed < DROP_TIME)
        {
            timePassed += TIME_TICK;
            meteor.transform.position -= new Vector3(0, Y_OFFSET * (TIME_TICK / DROP_TIME), 0);
            yield return new WaitForSeconds(TIME_TICK);
        }
        meteor.DealDamage(METEOR_DAMAGE);
        meteor.gameObject.SetActive(false);
        PlayerMotion.LockMotion(false);
    }
}
