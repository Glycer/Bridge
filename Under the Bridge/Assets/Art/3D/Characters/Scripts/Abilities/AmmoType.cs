using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoType : SecondaryAttack
{
    public int maxAmmo;
    protected int ammoCount;
    public AudioClip soundClip;
    public int manaFill;
    public int ammoStrength;

    public float shotDelay;
    public int firingType;
    public float chargeTime;

    public virtual void UseAmmo(MonsterStats enemy = null)
    {
    }
    public int ConsumeAmmo()
    {
        return --ammoCount;
    }
    public bool GetAmmo()
    {
        if (ammoCount > 0)
            return true;
        else
            return false;
    }

    public void LoadAmmo()
    {
        ammoCount = maxAmmo;
    }

    public int GetManaFill()
    {
        return manaFill;
    }

    public int GetStrength()
    {
        return ammoStrength;
    }

    public AudioClip GetAudioClip()
    {
        return soundClip;
    }

    public int GetFiringType()
    {
        return firingType;
    }

    public float GetDelay()
    {
        return shotDelay;
    }

    public float GetChargeTime()
    {
        return chargeTime;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
}
