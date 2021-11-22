using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Projectile that splits into fragments
public class ProjectileSplitter : Projectile
{
    public ProjectileLauncher launcher;
    public int numFragments;
    public Vector3 angle;

    // Possible additional variables
    //public float delay;
    //public float gap;

    protected override void Detonate(Collision collision)
    {
        LaunchFragments();
        base.Detonate(collision);
    }

    // Iterates through numFragments and launches projectiles with equidistant angles
    void LaunchFragments()
    {
        for (int i = 0; i < numFragments; i++)
        {
            launcher.Launch(transform.position, new Vector3(angle.x, 360 / numFragments * i, angle.z));
        }
    }
}
