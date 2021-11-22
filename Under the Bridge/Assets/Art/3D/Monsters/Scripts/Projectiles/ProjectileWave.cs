using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWave : Projectile
{
    public GameObject wavePrefab;

    protected override void Detonate(Collision collision)
    {
        GameObject wave = Instantiate(wavePrefab);
        wave.transform.position = transform.position;
        wave.GetComponent<Shockwave>().Detonate();
        base.Detonate(collision);
    }
}
