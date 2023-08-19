using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SecondaryAttack[] secondaries;
    // int weaponStrength

    public virtual void Attack(bool keyDown)
    {

    }
    public virtual void Secondary(int secondaryIndex)
    {

    }

    public SecondaryAttack[] GetSecondaries()
    {
        return secondaries;
    }
}
