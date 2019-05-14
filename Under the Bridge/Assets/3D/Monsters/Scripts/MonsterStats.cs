using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int hitPoints;
    public float moveSpeed;
    public int turnSpeed;

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

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
