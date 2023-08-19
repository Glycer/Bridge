using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoWeapon : Weapon
{
    public HanzoWeaponCollider[] colliders;
    // TODO: Combos should likely be made private once runtime customization is implemented
    public HanzoAttack[] combo;
    public static int currStrength;
    public static int currManaFill;

    public void SetWeaponActive(bool isActive)
    {
        foreach (HanzoWeaponCollider collider in colliders)
        {
            collider.SetWeaponActive(isActive);
        }
    }

    public HanzoAttack[] GetCombo()
    {
        return combo;
    }
}
