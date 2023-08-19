using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int strength;
    public float scale;
    public float speed;
    public int ttl;
    // If false, projectile dissapears on expire. If true, projectile detonates on expire.
    public bool autoDetonate;
    // For tracking, 100 rotationSpeed is instantaneous
    public float rotationSpeed;
    public Transform targetDirection;

    // Used for explosive shots
    public AreaOfEffect aoe;
    public GameObject visibleExplosion;

    Coroutine life;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != this.gameObject.layer)
            Detonate(collision);
    }
    protected virtual void Detonate(Collision collision = null)
    {
        // autoDetonate has served its purpose
        autoDetonate = false;

        if (collision != null && collision.gameObject.GetComponent<PlayerMotion>() && aoe == null)
            PlayerStats.TakeDamage(strength);
        if (aoe != null)
        {
            StartCoroutine(Explosion());
            aoe.Attack();
        }
        else
            Deactivate();
    }

    public void Activate()
    {
        transform.localScale *= scale;
        life = StartCoroutine(ProjectilePool.Live(this));
        ProjectilePool.Add(this);
    }

    public void Deactivate()
    {
        // autoDetonate will trigger if it exists and Detonate has not triggered
        if (autoDetonate)
            Detonate();
        else if (this != null)
            Destroy(gameObject);
    }

    // Used for tracking projectiles
    public void StartTracking(GameObject target)
    {
        StartCoroutine(Track(target));
    }
    IEnumerator Track(GameObject target)
    {
        while (true)
        {
            targetDirection.LookAt(target.transform);
            if (target.activeSelf)
               transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection.transform.rotation, rotationSpeed / 100);
            transform.Translate(0, 0, 1 * speed / 5000);
            yield return new WaitForSeconds(0.02f);
        }
    }

    // Makes projectile explode
    protected IEnumerator Explosion()
    {
        visibleExplosion.SetActive(true);
        yield return new WaitForSeconds(1);
        Deactivate();
    }
}
