using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MischiefMoveset : EnemyMotion
{
    Coroutine attack;
    Coroutine specificAttack;

    public override void Pursue()
    {
        isPursuing = true;
        StopCoroutine(patrol);

        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);

        move = StartCoroutine(ForwardMotion());
        turn = StartCoroutine(LookRotation(playerDirection.transform.rotation));
        attack = StartCoroutine(Attack());

        moveSpeed = stats.runSpeed;
        animControl.ToggleState("isRunning");
    }

    IEnumerator Attack()
    {
        // time to wait before next attack
        float timeCounter = 3;
        if (timeCounter <= 0)
            specificAttack = null;

        while (true)
        {
            if (timeCounter <= 0 && Vector3.Distance(playerDirection.playerInterest.target.transform.position, transform.position) < 4)
            {
                // pick random move depending on power of monster
                attack = StartCoroutine(Leap());
                timeCounter = Random.Range(2, 5);
            }

            yield return new WaitForSeconds(0.2f);
            timeCounter -= 0.2f;
        }
    }

    IEnumerator Leap()
    {
        for (int i = 0; i < 20; i++)
        {
            transform.Translate(0, 0.1f, 0.2f);
            yield return new WaitForSeconds(0.02f);
        }
    }

    public override void Halt()
    {
        moveSpeed = stats.walkSpeed;

        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);
        if (attack != null)
            StopCoroutine(attack);

        isPursuing = false;
        patrol = StartCoroutine(Patrol());
    }

}
