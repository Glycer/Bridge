using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwapCharacter : MonoBehaviour
{
    public static GameObject activeChar;

    public int initCharacterIndex;

    public List<GameObject> characters;

    public UIManager uiManager;

    // Disengage aim when swap
    public CamControl cameraAim;

    CharacterAnimControl animControl;
    static bool swapLocked;

    KeyCode[] cArray = new KeyCode[] { Inputs.swapIn1, Inputs.swapIn2, Inputs.swapIn3 };

    // Start is called before the first frame update
    void Start()
    {
        animControl = GetComponent<CharacterAnimControl>();

        Swap(characters[initCharacterIndex]);
        animControl.enabled = true;
        swapLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cArray.Length; i++)
            if (Input.GetKeyDown(cArray[i]) && !PlayerMotion.MotionLocked() && !swapLocked)
            {
                Swap(characters[i]);
                uiManager.SwitchCharacter(i);
            }
    }

    void Swap(GameObject c)
    {
        foreach (GameObject buddy in characters)
        {
            buddy.SetActive(buddy == c);
            if (buddy == c)
                buddy.GetComponent<PlayerSkills>().EnableChar();
        }

        activeChar = c;
        animControl.anim = c.GetComponent<Animator>();
        animControl.enabled = (c != characters[2]);
    }

    public static void LockSwap(bool isLocked)
    {
        swapLocked = isLocked;
    }
}
