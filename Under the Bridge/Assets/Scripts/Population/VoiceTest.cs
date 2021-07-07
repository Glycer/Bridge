using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTest : MonoBehaviour
{
    /* This works, now keep it in the cooler until the next test.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Citizen citizen = new Citizen();
            TestScene(citizen);
        }
    }
    */

    public void TestScene(Citizen citizen)
    {
        Debug.Log(DialogueDatabase.LovelyWeather(citizen));
    }
}
