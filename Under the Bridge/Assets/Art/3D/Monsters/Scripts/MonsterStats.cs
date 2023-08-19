using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterStats : MonoBehaviour
{
    public static UnityEvent HealthChange;

    public int hitPoints;
    int currHitPoints;
    public int strength;
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;
    public double weight;
    bool dead;

    public PlayerLoot.Loot lootType;
    public int lootNum;

    public void Awake()
    {
        if (HealthChange == null)
            HealthChange = new UnityEvent();
        currHitPoints = hitPoints;
    }

    public void Spawn(Vector3 destination)
    {
        dead = false;
        gameObject.transform.position = destination;
        gameObject.transform.Translate(0, 1, 0);
        gameObject.SetActive(true);
        currHitPoints = hitPoints;
    }
    public void DeSpawn()
    {
        gameObject.SetActive(false);
    }

    public void HealthAlert()
    {
        HealthChange.Invoke();
    }
    public virtual bool TakeDamage(int damage)
    {
        currHitPoints -= damage;
        HealthAlert();

        if (currHitPoints <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    void Die()
    {
        dead = true;
        gameObject.SetActive(false);
        PlayerLoot.UpdateLoot(lootType, lootNum);
    }
    public bool IsDead()
    {
        return dead;
    }

    public double GetWeight()
    {
        return weight;
    }

    public float GetHealthPercentage()
    {
        return (float)currHitPoints / (float)hitPoints;
    }
}
