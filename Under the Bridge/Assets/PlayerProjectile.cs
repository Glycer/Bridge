using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    public int manaIndex;
    public int manaFill;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MonsterStats>())
        {
            collision.gameObject.GetComponent<MonsterStats>().TakeDamage(strength);
            PlayerStats.AddMana(manaIndex, manaFill);
        }
        Deactivate();
    }
}
