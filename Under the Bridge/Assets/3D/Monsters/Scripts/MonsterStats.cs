using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int hitPoints;
    public float moveSpeed;

    public void Spawn(Vector3 destination)
    {
        gameObject.transform.position = destination;
        gameObject.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
            Die();
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
