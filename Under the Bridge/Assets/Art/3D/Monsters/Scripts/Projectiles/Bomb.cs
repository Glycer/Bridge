using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float scale;
    public int ttlBase;
    public int explosionTimeBase;
    public AreaAttack explosion;

    public void SetBomb(int ttl = 0, int explosionTime = 0)
    {
        transform.localScale *= scale;
        StartCoroutine(Timer(ttl == 0 ? ttlBase : ttl, explosionTime == 0 ? explosionTimeBase : explosionTime));
    }

    // Sets timer on bomb, timer is time until explosion, explosion timer is time that explosion lasts
    IEnumerator Timer(int timer, int explosionTimer)
    {
        yield return new WaitForSeconds(timer);
        explosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(explosionTimer);
        Destroy(gameObject);
    }
}

