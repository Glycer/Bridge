using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float scale;
    public float speed;
    public int ttl;

    Coroutine life;

    private void OnCollisionEnter(Collision collision)
    {
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
        if (this != null)
            Destroy(gameObject);
    }
}
