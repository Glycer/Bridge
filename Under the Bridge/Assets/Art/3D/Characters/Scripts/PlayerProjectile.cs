using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    public int manaIndex;
    public int manaFill;
    public TargetCollider blastRadius;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != this.gameObject.layer)
            Detonate(collision);
    }

    protected override void Detonate(Collision collision = null)
    {
        // autoDetonate has served its purpose
        autoDetonate = false;

        if (blastRadius != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(Explosion());
            foreach (Collider collider in blastRadius.targets)
            {
                if (collider.GetComponent<MonsterStats>() != null)
                    collider.GetComponent<MonsterStats>().TakeDamage(strength);
            }
        }
        else if (collision.gameObject.GetComponent<MonsterStats>())
        {
            collision.gameObject.GetComponent<MonsterStats>().TakeDamage(strength);
            PlayerStats.AddMana(manaIndex, manaFill);

            Deactivate();
        }
        else
            Deactivate();
    }
}
