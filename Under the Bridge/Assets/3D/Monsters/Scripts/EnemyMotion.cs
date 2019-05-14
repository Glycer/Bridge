using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotion : MonoBehaviour
{
    public float moveSpeed;
    public int turnSpeed;
    public bool isPursuing;
    public EnemyLockOn playerDirection;
    Coroutine look;
    Coroutine move;
    Coroutine turn;
    Coroutine patrol;
    Coroutine bounce;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = GetComponent<MonsterStats>().moveSpeed;
        turnSpeed = GetComponent<MonsterStats>().turnSpeed;
        patrol = StartCoroutine(Patrol());
    }

    public void Move()
    {
        isPursuing = true;
        StopCoroutine(patrol);
        StopCoroutine(move);
        StopCoroutine(turn);
        move = StartCoroutine(ForwardMotion());
        Debug.Log("Moving");
        turn = StartCoroutine(LookRotation(playerDirection.transform.rotation));
    }

    public void Halt()
    {
        StopCoroutine(move);
        StopCoroutine(turn);
        isPursuing = false;
        new WaitForSeconds(3);
        patrol = StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            turn = StartCoroutine(LookRotation(Quaternion.Euler(0, Random.Range(0, 360), Random.Range(0, 360))));
            yield return new WaitForSeconds(Random.Range(1, 1.5f));
            StopCoroutine(turn);
            move = StartCoroutine(ForwardMotion());
            yield return new WaitForSeconds(Random.Range(2, 4));
            StopCoroutine(move);
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    // primary pursuing motion function
    IEnumerator ForwardMotion()
    {
        while (true)
        {
            transform.Translate(0, 0, moveSpeed / 60);

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    IEnumerator LookRotation(Quaternion toGo)
    {
        while (true)
        {
            TurnAround(0.1f, toGo);

            yield return new WaitForSeconds(0.1f);
        }
    }

    void TurnAround(float baseTurnSpeed, Quaternion toLook)
    {
        float duration = baseTurnSpeed / turnSpeed;

        if (look != null)
            StopCoroutine(look);

        look = StartCoroutine(Interpolater.InterpolateLocalRotation(this.transform, toLook, duration));
    }

    public void HorizontalCollision()
    {
        if (patrol != null)
            StopCoroutine(patrol);

        if (move != null)
            StopCoroutine(move);

        if (turn != null)
            StopCoroutine(turn);

        bounce = StartCoroutine(BounceBack());
    }

    IEnumerator BounceBack()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.Translate(0, 0, -moveSpeed / 60);

            yield return new WaitForSeconds(1 / 60f);
        }
        patrol = StartCoroutine(Patrol());
    }
}
