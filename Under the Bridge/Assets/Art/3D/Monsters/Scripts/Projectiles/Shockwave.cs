using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used by ProjectileWave
public class Shockwave : MonoBehaviour
{
    // Controls speed of expansion, contraction. Positive speed expands, negative speed contracts.
    public float waveSpeed;
    public float scale;
    public int strength;
    // Time to live
    public float ttl;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMotion>() != null)
            PlayerStats.TakeDamage(strength);
    }

    public void Detonate()
    {
        transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z);
        StartCoroutine(Wave());
    }

    IEnumerator Wave()
    {
        for (float i = 0; i < ttl; i += 0.02f)
        {
            transform.localScale += new Vector3(waveSpeed, waveSpeed, 0);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
}
