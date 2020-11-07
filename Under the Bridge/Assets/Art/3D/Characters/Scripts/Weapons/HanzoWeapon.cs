using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoWeapon : MonoBehaviour
{
    public static int currStrength;
    public static int currManaFill;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Hit!");
        PlayerStats.AddMana(0, currManaFill);
        if (collider.gameObject.GetComponent<MonsterStats>())
            collider.gameObject.GetComponent<MonsterStats>().TakeDamage(currStrength);
    }
}
