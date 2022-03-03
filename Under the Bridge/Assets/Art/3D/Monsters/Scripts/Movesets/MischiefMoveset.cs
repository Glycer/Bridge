using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MischiefMoveset : EnemyMotion
{
    Coroutine attack;
    Coroutine specificAttack;
    public AreaOfEffect aoe;

    public override void Pursue()
    {
        isPursuing = true;
        StopCoroutine(patrol);

        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);

        move = StartCoroutine(ForwardMotion());
        facePlayer = StartCoroutine(LookRotation(0.02f, playerDirection.transform.rotation));
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
            if (timeCounter <= 0 && playerDirection.playerInterest.target != null && Vector3.Distance(playerDirection.playerInterest.target.transform.position, transform.position) < 4)
            {
                int randomInt = Random.Range(0, 2);
                // pick random move
                if (randomInt == 0)
                    specificAttack = StartCoroutine(Leap(Vector3.Distance(playerDirection.playerInterest.target.transform.position, transform.position)));
                else if (randomInt == 1)
                    specificAttack = StartCoroutine(Pounce(Vector3.Distance(playerDirection.playerInterest.target.transform.position, transform.position)));
                timeCounter = Random.Range(2, 5) + randomInt;
            }

            yield return new WaitForSeconds(0.2f);
            timeCounter -= 0.2f;
        }
    }

    IEnumerator Leap(float distance)
    {
        Vector3 forward = (transform.TransformVector(0, 0, 1) * distance / 24) + new Vector3(0, 0.1f, 0);
        for (int i = 0; i < 20; i++)
        {
            transform.position += forward;
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator Pounce(float distance)
    {
        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);

        yield return new WaitForSeconds(1);
        Vector3 forward = (transform.TransformVector(0, 0, 1) * distance / 10) + new Vector3(0, 0.1f, 0);
        for (int i = 0; i < 10; i++)
        {
            transform.position += forward;
            yield return new WaitForSeconds(0.02f);
        }
        aoe.Attack();
        yield return new WaitForSeconds(1.5f);
        move = StartCoroutine(ForwardMotion());
        facePlayer = StartCoroutine(LookRotation(0.02f, playerDirection.transform.rotation));
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
        if (specificAttack != null)
            StopCoroutine(specificAttack);

        isPursuing = false;
        patrol = StartCoroutine(Patrol());
    }

}
