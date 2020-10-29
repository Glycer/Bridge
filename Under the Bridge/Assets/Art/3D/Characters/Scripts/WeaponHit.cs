using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    public PlayerStats player;
    public Collider enemy;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Hit!");
        player.AddMana(0, 5);
        collider.gameObject.GetComponent<MonsterStats>().TakeDamage(1);
    }
}
