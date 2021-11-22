using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBoss : MonsterStats
{
    public HumanForm human;
    public CatForm cat;
    public BirdForm bird;

    // For aiming
    public Transform player;
    public Transform focus;

    // For returning to center
    public Transform centerPosition;

    public Transform airPosition;

    public void StartCombat()
    {
        EnterCat();
    }

    // Enter cat form
    public void EnterCat()
    {
        human.gameObject.SetActive(false);
        cat.gameObject.SetActive(true);
        cat.Attack();
    }
    // Enter bird form
    public void EnterBird()
    {
        StartCoroutine(TransitionBird());
    }
    // Enter human form
    public void EnterHuman()
    {
        bird.gameObject.SetActive(false);
        human.gameObject.SetActive(true);
        human.Attack();
    }

    IEnumerator TransitionBird()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = centerPosition.position;
        for (int i = 0; i < 50; i++)
        {
            transform.position = Vector3.Lerp(startPos, new Vector3(targetPos.x, transform.position.y, targetPos.z), (float)i / 50);
            yield return new WaitForSeconds(0.02f);
        }
        cat.gameObject.SetActive(false);
        bird.gameObject.SetActive(true);
        // Fly up
        startPos = transform.position;
        targetPos = airPosition.position;
        for (int i = 0; i < 50; i++)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, (float)i / 50);
            yield return new WaitForSeconds(0.02f);
        }
        bird.Attack();
    }

    public override bool TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            human.gameObject.SetActive(false);
            cat.gameObject.SetActive(false);
            bird.gameObject.SetActive(false);
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}
