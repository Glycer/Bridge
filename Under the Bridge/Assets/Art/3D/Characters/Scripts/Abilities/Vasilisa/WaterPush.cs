using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPush : Ability
{
    public TargetCollider targetCollider;
    public Transform targetColliderRoot;

    public Animator charAnim;
    public Animator fxAnim;

    const float PULL_STRENGTH = 1000;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown)
        {
            charAnim.SetBool("isPushing", true);
            charAnim.SetInteger("idleBreakNum", 0);
            fxAnim.Play("WaterPush");

            targetCollider.RefreshList();
            foreach (Collider target in targetCollider.targets)
            {
                if (target.GetComponent<Rigidbody>())
                {
                    targetColliderRoot.LookAt(target.transform);
                    target.GetComponent<Rigidbody>().AddForce(targetColliderRoot.TransformDirection(Vector3.forward) * PULL_STRENGTH);
                }
            }
            targetColliderRoot.localRotation = Quaternion.identity;
            PlayerStats.SpendMana(manaCost);
        }
    }
}
