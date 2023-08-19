using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryAttack : MonoBehaviour
{
    public Sprite abilityImage;
    public float lockTime;
    // For Hanzo
    public HanzoAttack hanzoAttack;

    public float GetLockTime()
    {
        return lockTime;
    }

    public virtual void UseAttack(bool Keydown)
    {
    }

    public HanzoAttack GetHanzoAttack()
    {
        return hanzoAttack;
    }
}
