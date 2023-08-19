using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotion : MonoBehaviour
{
    public bool isPursuing;
    public EnemyLockOn playerDirection;

    protected float moveSpeed;
    float turnSpeed;
    const float BASE_TURN_SPEED = 100;

    protected Coroutine facePlayer;
    protected Coroutine move;
    protected Coroutine turn;
    protected Coroutine patrol;
    Coroutine bounce;

    protected MonsterAnimControl animControl;
    protected MonsterStats stats;

    // Start is called before the first frame update
    void OnEnable()
    {
        animControl = GetComponent<MonsterAnimControl>();
        stats = GetComponent<MonsterStats>();
        moveSpeed = stats.walkSpeed;
        turnSpeed = stats.turnSpeed;
        patrol = StartCoroutine(Patrol());
    }

    // Starts pursuing the player and fights, used by moveset scripts
    public virtual void Pursue()
    {
        isPursuing = true;
        StopCoroutine(patrol);

        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);

        move = StartCoroutine(ForwardMotion());
        facePlayer = StartCoroutine(LookRotation(Quaternion.Euler(0, playerDirection.playerInterest.transform.eulerAngles.y, 0)));

        moveSpeed = stats.runSpeed;
        animControl.ToggleState("isRunning");
    }

    // Lost the player
    public virtual void Halt()
    {
        moveSpeed = stats.walkSpeed;

        if (move != null)
            StopCoroutine(move);
        if (turn != null)
            StopCoroutine(turn);
        if (facePlayer != null)
            StopCoroutine(facePlayer);

        isPursuing = false;
        patrol = StartCoroutine(Patrol());
    }

    // Random walk
    protected virtual IEnumerator Patrol()
    {
        while (true)
        {
            TurnAround(Quaternion.Euler(0, Random.Range(0, 360), 0));
            yield return new WaitForSeconds(Random.Range(1, 1.5f));
            if (turn != null)
                StopCoroutine(turn);
            move = StartCoroutine(ForwardMotion());
            animControl.ToggleState("isWalking");
            yield return new WaitForSeconds(Random.Range(2, 4));
            StopCoroutine(move);
            animControl.ToggleState("isIdle");
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    // Moves enemy forward
    protected IEnumerator ForwardMotion()
    {
        while (true)
        {
            transform.Translate(0, 0, moveSpeed / 60);

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    // Turns enemy to face player
    protected IEnumerator LookRotation(Quaternion toGo, int speedModifier = 1)
    {
        while (true)
        {
            TurnAround(toGo, speedModifier);

            toGo = Quaternion.Euler(0, playerDirection.playerInterest.transform.eulerAngles.y, 0);
            yield return new WaitForSeconds(0.025f);
        }
    }
    // Turns enemy
    protected void TurnAround(Quaternion toLook, float speedmodifier = 1)
    {
        // duration = distance over speed over modifier
        float duration = System.Math.Abs(transform.eulerAngles.y - toLook.eulerAngles.y) / BASE_TURN_SPEED / turnSpeed / speedmodifier;

        if (turn != null)
            StopCoroutine(turn);

        if (duration != 0)
            turn = StartCoroutine(Interpolater.InterpolateLocalRotation(transform, toLook, duration));
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
