using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwapCharacter : MonoBehaviour
{
    public static GameObject activeChar;

    public int initCharacterIndex;

    public List<GameObject> characters;

    CharacterAnimControl animControl;

    KeyCode[] cArray = new KeyCode[] { Inputs.swapIn1, Inputs.swapIn2, Inputs.swapIn3 };

    // Start is called before the first frame update
    void Start()
    {
        animControl = GetComponent<CharacterAnimControl>();

        Swap(characters[initCharacterIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cArray.Length; i++)
            if (Input.GetKeyDown(cArray[i]))
                Swap(characters[i]);
    }

    void Swap(GameObject c)
    {
        foreach (GameObject buddy in characters)
            buddy.SetActive(buddy == c);

        activeChar = c;
        animControl.anim = c.GetComponent<Animator>();
        animControl.enabled = (c != characters[2]);
    }
}
