using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectilePool
{
    public const int CAPACITY = 10;
    public static Queue<Projectile> active = new Queue<Projectile>();

    public static void Add(Projectile projectile)
    {
        active.Enqueue(projectile);

        while (active.Count > CAPACITY)
        {
            Projectile oldest = active.Dequeue();
            oldest.Deactivate();
        }
    }

    public static IEnumerator Live(Projectile projectile)
    {
        for (int i = 0; i < projectile.ttl; i++)
            yield return new WaitForSeconds(1);
        
        projectile.Deactivate();
    }
}
