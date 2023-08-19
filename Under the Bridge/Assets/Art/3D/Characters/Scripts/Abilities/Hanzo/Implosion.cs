using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Implosion : Ability
{
    public TargetCollider targetCollider;

    const float PULL_STRENGTH = 500;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown)
        {
            targetCollider.RefreshList();
            foreach (Collider target in targetCollider.targets)
            {
                if (target.GetComponent<Rigidbody>())
                {
                    targetCollider.transform.LookAt(target.transform);
                    target.GetComponent<Rigidbody>().AddForce(targetCollider.transform.TransformDirection(Vector3.back) * PULL_STRENGTH);
                }
            }
            PlayerStats.SpendMana(manaCost);
        }
    }
}