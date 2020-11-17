using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaStaff : Weapon
{
    public TargetCollider enemiesInPushRange;
    public TargetCollider enemiesInProjectileRange;
    public Transform launchPosition;
    public ProjectileLauncher projectile;

    public bool attackLocked;

    public override void Primary()
    {
        if (!attackLocked)
        {
            // Add a branch for lock on
            for (int i = 0; i < enemiesInProjectileRange.targets.Count; i++)
            {
                if (!enemiesInProjectileRange.targets[i].gameObject.activeSelf)
                    enemiesInProjectileRange.targets.Remove(enemiesInProjectileRange.targets[i]);
            }
            if (enemiesInProjectileRange.targets.Count != 0)
            {
                GameObject enemy = enemiesInProjectileRange.targets[Random.Range(0, enemiesInProjectileRange.targets.Count)].gameObject;
                projectile.Launch(launchPosition.position, launchPosition.rotation.eulerAngles, true, enemy);
            }
            else
                projectile.Launch(launchPosition.position, launchPosition.rotation.eulerAngles);
            StartCoroutine(LockAttack());
        }
    }
    IEnumerator LockAttack()
    {
        attackLocked = true;
        yield return new WaitForSeconds(0.5f);
        attackLocked = false;
    }

    public override void Secondary()
    {
        foreach (Collider enemy in enemiesInPushRange.targets)
        {
            enemiesInPushRange.transform.LookAt(enemy.transform);
            enemy.gameObject.GetComponent<Rigidbody>().AddForce(enemiesInPushRange.transform.forward * 500);
        }
    }
}
