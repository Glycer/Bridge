using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoWeaponCollider : MonoBehaviour
{
    // Bool sets whether weapon does damage in that moment
    bool weaponActive;

    public void SetWeaponActive(bool isActive)
    {
        weaponActive = isActive;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (weaponActive)
        {
            PlayerStats.AddMana(0, HanzoWeapon.currManaFill);
            if (collider.gameObject.GetComponent<MonsterStats>())
                collider.gameObject.GetComponent<MonsterStats>().TakeDamage(HanzoWeapon.currStrength);
        }
    }
}
