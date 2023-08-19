using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaProjectile : VasilisaAttack
{
    public CamLockOn lockOn;
    public ProjectileLauncher projectile;
    public TargetCollider enemiesInProjectileRange;

    public bool isTracking;

    public override void UseAttack(bool keyDown)
    {
        if (keyDown)
        {
            enemiesInProjectileRange.RefreshList();
            // Fires at locked on target if locked on
            if (lockOn.camControl.locked)
                projectile.Launch(attackPosition.position, attackPosition.rotation.eulerAngles, isTracking, lockOn.targetLocation.gameObject);
            // Fires at random nearby enemy otherwise
            else if (enemiesInProjectileRange.targets.Count != 0)
            {
                enemiesInProjectileRange.RefreshList();
                GameObject enemy = enemiesInProjectileRange.targets[Random.Range(0, enemiesInProjectileRange.targets.Count)].gameObject;
                projectile.Launch(attackPosition.position, attackPosition.rotation.eulerAngles, isTracking, enemy);
            }
            else
                projectile.Launch(attackPosition.position, attackPosition.rotation.eulerAngles);
        }
    }
}
