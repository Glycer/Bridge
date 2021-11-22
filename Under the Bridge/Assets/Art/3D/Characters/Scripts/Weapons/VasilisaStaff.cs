using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaStaff : Weapon
{
    public TargetCollider enemiesInPushRange;
    public TargetCollider enemiesInProjectileRange;
    public Transform launchPosition;
    public ProjectileLauncher projectile;
    public CamLockOn lockOn;

    public bool attackLocked;

    void OnEnable()
    {
        attackLocked = false;
    }

    public override void Primary()
    {
        if (!attackLocked)
        {
            enemiesInProjectileRange.RefreshList();
            // Fires at locked on target if locked on
            if (lockOn.camControl.locked)
                projectile.Launch(launchPosition.position, launchPosition.rotation.eulerAngles, true, lockOn.targetLocation.gameObject);
            // Fires at random nearby enemy otherwise
            else if (enemiesInProjectileRange.targets.Count != 0)
            {
                enemiesInProjectileRange.RefreshList();
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
            if (enemy.gameObject.GetComponent<Rigidbody>() != null)
            {
                enemiesInPushRange.transform.LookAt(enemy.transform);
                enemy.gameObject.GetComponent<Rigidbody>().AddForce(enemiesInPushRange.transform.forward * 500);
            }
        }
    }
}
