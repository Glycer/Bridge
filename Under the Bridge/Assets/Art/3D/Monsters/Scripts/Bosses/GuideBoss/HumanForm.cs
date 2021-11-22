using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanForm : MonoBehaviour
{
    public GuideBoss guideBoss;

    public GameObject waterJet;
    public GameObject spinJet;
    public GameObject[] geyserTelegraphs;
    public GameObject[] geysers;
    // Used for balloon instantiation and activation
    public GameObject balloon;
    Bomb currBalloon;
    // Used for balloon detonation
    int currTime;

    System.Random random;

    public void Attack()
    {
        random = new System.Random();
        StartCoroutine(Fight());
    }

    IEnumerator Fight()
    {
        currTime = 23;

        StartCoroutine(FireGeysers());
        yield return new WaitForSeconds(2);
        StartCoroutine(WaterJet());
        yield return new WaitForSeconds(5);
        StartCoroutine(SpinJet());
        yield return new WaitForSeconds(20);
        guideBoss.EnterCat();
    }

    // Sets geysers at random positions and fires them
    IEnumerator FireGeysers()
    {
        int j;
        for (int i = 0; i < 5; i++)
        {
            for (j = 0; j < geysers.Length; j++)
            {
                // Set to a random position
                geyserTelegraphs[j].transform.position = guideBoss.centerPosition.position + new Vector3(((float)random.NextDouble() - 0.5f) * 18, 0, ((float)random.NextDouble() - 0.5f) * 18);
                geysers[j].transform.position = geyserTelegraphs[j].transform.position;
                geyserTelegraphs[j].SetActive(true);
            }
            yield return new WaitForSeconds(1);
            foreach (GameObject geyser in geyserTelegraphs)
            {
                geyser.SetActive(false);
            }
            foreach (GameObject geyser in geysers)
            {
                geyser.SetActive(true);
            }
            yield return new WaitForSeconds(1);
            // Disable geysers and start balloons
            foreach (GameObject geyser in geysers)
            {
                geyser.SetActive(false);

                currBalloon = Instantiate(balloon).GetComponent<Bomb>();
                currBalloon.transform.position = geyser.transform.position + new Vector3(0, 0.5f, 0);
                currBalloon.gameObject.SetActive(true);
                currBalloon.SetBomb(currTime, 3);
            }
            currTime -= 4;
            yield return new WaitForSeconds(2);
        }
    }

    // Fires tracking jet
    IEnumerator WaterJet()
    {
        waterJet.SetActive(true);
        // yield return new WaitForSeconds(20);
        for (float i = 0; i < 20; i += 0.02f)
        {
            guideBoss.focus.LookAt(guideBoss.player);
            waterJet.transform.rotation = Quaternion.Lerp(waterJet.transform.rotation, guideBoss.focus.rotation, 0.05f);
            waterJet.transform.eulerAngles = new Vector3(90, waterJet.transform.eulerAngles.y, waterJet.transform.eulerAngles.z);
            yield return new WaitForSeconds(0.02f);
        }
        waterJet.SetActive(false);
    }

    // Fires spinning jet
    IEnumerator SpinJet()
    {
        spinJet.transform.eulerAngles = new Vector3(90, 0, waterJet.transform.eulerAngles.z);
        spinJet.SetActive(true);
        //yield return new WaitForSeconds(15);
        for (float i = 0; i < 15; i += 0.02f)
        {
            spinJet.transform.eulerAngles += new Vector3(0, 0, 0.5f);
            yield return new WaitForSeconds(0.02f);
        }
        spinJet.SetActive(false);
    }
}
