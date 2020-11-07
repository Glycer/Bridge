using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int strength;
    public float scale;
    public float speed;
    public int ttl;
    // For tracking, 100 rotationSpeed is instantaneous
    public float rotationSpeed;
    public Transform targetDirection;

    Coroutine life;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMotion>())
            PlayerStats.TakeDamage(strength);
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
}
