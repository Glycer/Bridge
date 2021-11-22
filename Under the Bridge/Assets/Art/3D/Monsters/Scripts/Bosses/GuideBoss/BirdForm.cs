using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdForm : MonoBehaviour
{
    public GuideBoss guideBoss;

    public ProjectileLauncher birdSplitter;
    public ProjectileLauncher birdShock;

    Coroutine perpendicular;

    System.Random random;
    int randInt;

    // For hitscan shots, TEMPORARY
    public GameObject telegraph;

    public void Attack()
    {
        random = new System.Random();
        StartCoroutine(Fight());
    }

    IEnumerator Fight()
    {
        yield return new WaitForSeconds(1);
        // Displace from center, then fly in a circle
        for (int i = 0; i < 50; i++)
        {
            guideBoss.transform.position += guideBoss.transform.TransformDirection(transform.forward) / 7;
            yield return new WaitForSeconds(0.02f);
        }
        if (perpendicular != null)
            StopCoroutine(perpendicular);
        perpendicular = StartCoroutine(AimPerpendicular());
        // The boss first fires projectiles while flying in a circle
        StartCoroutine(FireProjectile());
        for (int i = 0; i < 750; i++)
        {
            guideBoss.transform.position += guideBoss.transform.TransformDirection(transform.forward) / 7;
            yield return new WaitForSeconds(0.02f);
        }
        // The boss then moves into the center and fires hitscan shots
        StopCoroutine(perpendicular);
        guideBoss.transform.position = guideBoss.centerPosition.position + new Vector3(0, 5, 0);
        // Fire three times
        for (int i = 0; i < 3; i++)
        {
            // Wait random time before firing
            yield return new WaitForSeconds((float)random.NextDouble() * 3 + 2);
            telegraph.SetActive(true);
            yield return new WaitForSeconds(1);
            // TODO: Refine damage
            PlayerStats.TakeDamage(10, true);
            telegraph.SetActive(false);
        }

        // Transition
        guideBoss.transform.position = guideBoss.centerPosition.position + new Vector3(0, 1, 0);
        guideBoss.EnterHuman();
    }
    // Aims bird perpendicular to the center for circular motion
    IEnumerator AimPerpendicular()
    {
        while (true)
        {
            guideBoss.focus.LookAt(guideBoss.airPosition);
            transform.rotation = guideBoss.focus.rotation * Quaternion.Euler(0, 90, 0);
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator FireProjectile()
    {
        for (int i = 0; i < 5; i++)
        {
            guideBoss.focus.LookAt(guideBoss.player);
            randInt = random.Next(0, 2);
            switch (randInt)
            {
                case 0:
                    birdSplitter.Launch(transform.position, guideBoss.focus.rotation.eulerAngles);
                    break;
                case 1:
                    birdShock.Launch(transform.position, guideBoss.focus.rotation.eulerAngles);
                    break;
            }
            yield return new WaitForSeconds(3);
        }
    }
}
