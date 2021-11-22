using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMoveset : EnemyMotion
{
    public ProjectileLauncher standardShot;
    public ProjectileLauncher explodingShot;
    public Transform LaunchPosition;
    Coroutine attack;

    public override void Pursue()
    {
        isPursuing = true;
        StopCoroutine(patrol);

        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);

        //move = StartCoroutine(ForwardMotion());
        turn = StartCoroutine(LookRotation(playerDirection.transform.rotation, 0.02f));
        attack = StartCoroutine(Attack());
    }

    // Random walk
    protected override IEnumerator Patrol()
    {
        while (true)
        {
            turn = StartCoroutine(LookRotation(Quaternion.Euler(0, Random.Range(0, 360), 0), 0.1f));
            yield return new WaitForSeconds(Random.Range(1, 1.5f));
            StopCoroutine(turn);
            move = StartCoroutine(ForwardMotion());
            yield return new WaitForSeconds(Random.Range(2, 4));
            StopCoroutine(move);
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    IEnumerator Attack()
    {
        // time to wait before next attack
        float timeCounter = 3;

        while (true)
        {
            if (timeCounter <= 0 && playerDirection.playerInterest.target != null)
            {
                int randomInt = Random.Range(0, 3);
                // pick random move
                if (randomInt < 2)
                    LaunchStandardProjectile();
                else if (randomInt == 2)
                    LaunchExplodingProjectile();
                timeCounter = Random.Range(2, 3) + randomInt;
            }

            yield return new WaitForSeconds(1);
            timeCounter -= 1;
        }
    }

    void LaunchStandardProjectile()
    {
        standardShot.Launch(LaunchPosition.position, transform.eulerAngles);
    }
    void LaunchExplodingProjectile()
    {
        explodingShot.Launch(LaunchPosition.position, transform.eulerAngles);
    }
}
