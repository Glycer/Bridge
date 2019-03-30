using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimControl : MonoBehaviour
{
    public Animator anim;

    Coroutine idleBreak;

    private void Start()
    {
        idleBreak = StartCoroutine(Spin());
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("isJumping"))
        {
            if (Input.GetAxis(Inputs.playerHAxis) != 0 || Input.GetAxis(Inputs.playerVAxis) != 0 || Input.GetAxis(Inputs.playerStrafeAxis) != 0)
                if (Input.GetKey(Inputs.sprint))
                    ToggleBool("isRunning");
                else
                    ToggleBool("isWalking");
            else
                ToggleBool("isIdling");
        }
    }

    void ToggleBool(string state)
    {
        string[] bools = new string[] { "isRunning", "isWalking", "isJumping", "isIdling", "isDoubleJumping" };

        foreach (string b in bools)
            anim.SetBool(b, b == state);
    }

    public void Jump(bool isJumping)
    {
        if (isJumping)
            anim.SetBool("isDoubleJumping", true);
        else
            ToggleBool("isJumping");
    }

    IEnumerator Spin()
    {
        while (true)
        {
            if (anim.GetBool("isIdling") && Random.Range(0, 10) == 0)
                anim.SetBool("isIdleBreak", true);

            yield return new WaitForSeconds(2);
        }
    }
}
