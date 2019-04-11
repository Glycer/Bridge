using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    public GameObject character;

    public List<GameObject> characters;

    KeyCode[] cArray = new KeyCode[] { Inputs.swapIn1, Inputs.swapIn2, Inputs.swapIn3 };

    // Start is called before the first frame update
    void Start()
    {
        Swap(characters[1]);
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
    }
}
