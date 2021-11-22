using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatForm : MonoBehaviour
{
    public GuideBoss guideBoss;
    System.Random random;
    public GameObject[] frontPaws;
    public GameObject[] rearPaws;
    public GameObject[] tail;
    public GameObject[] attacks;

    // For turning off telegraph
    GameObject[] currTelegraph;

    int randInt;

    Vector3 startPos;
    Vector3 targetPos;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Attack()
    {
        random = new System.Random();
        StartCoroutine(Fight());
    }

    IEnumerator Fight()
    {
        // Attack 6 times
        for (int i = 0; i < 6; i++)
        {
            randInt = random.Next(0, 3);
            // TODO: Random dash attack: Claw, front fire. Hindlegs, rear fire with front bite. Tail, circular fire.
            yield return new WaitForSeconds(1);
            // Display telegraph and rotate
            StartCoroutine(TurnToFace());
            switch (randInt)
            {
                // Claw
                case 0:
                    currTelegraph = frontPaws;
                    frontPaws[0].SetActive(true);
                    frontPaws[1].SetActive(true);
                    break;
                // Hind
                case 1:
                    currTelegraph = rearPaws;
                    rearPaws[0].SetActive(true);
                    rearPaws[1].SetActive(true);
                    break;
                // Tail
                case 2:
                    currTelegraph = tail;
                    tail[0].SetActive(true);
                    break;
            }
            yield return new WaitForSeconds(1);
            // Set up dash target
            startPos = guideBoss.transform.position;
            targetPos = guideBoss.player.position;
            distance = System.Math.Abs(startPos.x - targetPos.x) + System.Math.Abs(startPos.z - targetPos.z);
            // Dash and start attack
            switch (randInt)
            {
                case 0:
                    StartCoroutine(Dash(0));
                    break;
                case 1:
                    StartCoroutine(Dash(1));
                    break;
                case 2:
                    StartCoroutine(Dash(2));
                    break;
            }
            // Wait for attack to finish
            yield return new WaitForSeconds(2);
            // Return
            StartCoroutine(DashBack());
            yield return new WaitForSeconds(2);
        }
        // Transition
        guideBoss.EnterBird();
    }
    IEnumerator TurnToFace()
    {
        Quaternion startRot = transform.rotation;
        guideBoss.focus.LookAt(guideBoss.player);
        Quaternion targetRot = guideBoss.focus.rotation;
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.02f);
            transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(startRot.eulerAngles.x, targetRot.eulerAngles.y, startRot.eulerAngles.z), (float)i / 25);
        }
    }
    // Dash to player
    IEnumerator Dash(int attack)
    {
        for (float i = 0; i < distance; i += 0.8f)
        {
            guideBoss.transform.position = Vector3.Lerp(startPos, new Vector3(targetPos.x, guideBoss.transform.position.y, targetPos.z), i / distance);
            yield return new WaitForSeconds(0.02f);
        }
        // Turn off telegraph
        foreach (GameObject telegraph in currTelegraph)
        {
            telegraph.SetActive(false);
        }
        attacks[attack].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attacks[attack].SetActive(false);
    }
    // Return to center
    IEnumerator DashBack()
    {
        for (float i = 0; i < 1; i += 0.02f)
        {
            guideBoss.transform.position = Vector3.Lerp(guideBoss.transform.position, new Vector3(guideBoss.centerPosition.transform.position.x,
                guideBoss.transform.position.y, guideBoss.centerPosition.transform.position.z), i);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
