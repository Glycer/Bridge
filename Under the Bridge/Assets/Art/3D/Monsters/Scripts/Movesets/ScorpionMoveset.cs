using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionMoveset : EnemyMotion
{
    Coroutine attack;
    public AreaAttack blast;
    public AreaAttack laser;
    public GameObject tail;

    public override void Pursue()
    {
        isPursuing = true;
        StopCoroutine(patrol);

        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);

        move = StartCoroutine(ForwardMotion());
        facePlayer = StartCoroutine(LookRotation(playerDirection.transform.rotation));
        attack = StartCoroutine(Attack());

        moveSpeed = stats.runSpeed;
    }

    IEnumerator Attack()
    {
        // time to wait before next attack
        float timeCounter = 6;

        while (true)
        {
            if (timeCounter <= 0 && playerDirection.playerInterest.target != null && Vector3.Distance(playerDirection.playerInterest.target.transform.position, transform.position) < 2)
            {
                StartCoroutine(Blast());
                timeCounter = Random.Range(4, 6);

                if (move != null)
                    StopCoroutine(move);
                if (turn != null)
                    StopCoroutine(turn);
            }
            else if (timeCounter <= 0)
            {
                StartCoroutine(Laser());
                timeCounter = Random.Range(6, 8);

                if (move != null)
                    StopCoroutine(move);
                if (turn != null)
                    StopCoroutine(turn);
            }

            yield return new WaitForSeconds(0.2f);
            timeCounter -= 0.2f;
        }
    }

    IEnumerator Blast()
    {
        // Halt motion
        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);

        blast.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        blast.gameObject.SetActive(false);

        move = StartCoroutine(ForwardMotion());
        facePlayer = StartCoroutine(LookRotation(playerDirection.transform.rotation));
    }
    IEnumerator Laser()
    {
        // Halt motion
        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);

        laser.gameObject.SetActive(true);
        for (float i = 0; i < 240; i++)
        {
            tail.transform.eulerAngles = new Vector3(0, tail.transform.eulerAngles.y + 1.5f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        tail.transform.localEulerAngles = Vector3.zero;
        laser.gameObject.SetActive(false);

        move = StartCoroutine(ForwardMotion());
        facePlayer = StartCoroutine(LookRotation(playerDirection.transform.rotation));
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
