using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int hitPoints;
    public int strength;
    public float walkSpeed;
    public float runSpeed;
    public int turnSpeed;

    public PlayerLoot.Loot lootType;
    public int lootNum;

    public void Spawn(Vector3 destination)
    {
        gameObject.transform.position = destination;
        gameObject.transform.Translate(0, 1, 0);
        gameObject.SetActive(true);
    }

    public bool TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    void Die()
    {
        gameObject.SetActive(false);
        PlayerLoot.UpdateLoot(lootType, lootNum);
    }
}
