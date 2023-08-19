using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipStrike : Ability
{
    public TargetCollider targetCollider;
    public CamLockOn lockOn;
    public PlayerMotion playerMotion;
    public CharacterMotion characterMotion;

    const int ABILITY_DAMAGE = 3;
    const float Z_OFFSET = 1;
    const float ZIP_TIME = 0.3f;
    const float TIME_TICK = 0.02f;

    public override void UseAbility(bool keyDown)
    {
        if (keyDown && lockOn.IsLockedOn())
        {
            playerMotion.FaceEnemy();
            PlayerMotion.LockMotion(true);
            StartCoroutine(Zip());
        }
    }

    IEnumerator Zip()
    {
        float timePassed = 0;
        float distance = Vector3.Distance(playerMotion.transform.position, lockOn.GetTarget().position);
        while (timePassed < ZIP_TIME)
        {
            yield return new WaitForSeconds(TIME_TICK);
            playerMotion.transform.position += characterMotion.transform.TransformDirection(Vector3.forward) * (distance - Z_OFFSET) * (TIME_TICK / ZIP_TIME);
            timePassed += TIME_TICK;
        }
        targetCollider.DealDamage(ABILITY_DAMAGE);
        PlayerStats.SpendMana(manaCost);
        PlayerMotion.LockMotion(false);
    }
}
