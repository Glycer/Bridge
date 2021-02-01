using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimControl : MonoBehaviour
{
    public Animator anim;

    string[] bools = new string[] { "isIdling", "isRunning", "isWalking", "isJumping", "isDoubleJumping" };

    private void Start()
    {
        StartCoroutine(IdleBreak());
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("isJumping"))
        {
            if (Input.GetAxis(Inputs.playerHAxis) != 0 || Input.GetAxis(Inputs.playerVAxis) != 0) //|| Input.GetAxis(Inputs.playerStrafeAxis) != 0)
                if (Input.GetKey(Inputs.sprint))
                    ToggleState("isRunning");
                else
                    ToggleState("isWalking");
            else
                ToggleState();
        }
    }

    public void ToggleState(string state)
    {
        foreach (string b in bools)
            anim.SetBool(b, b == state);

        anim.SetInteger("idleBreakNum", 0);
    }

    public void ToggleState()
    {
        foreach (string b in bools)
            anim.SetBool(b, b == bools[0]);
    }

    public void Jump(bool isJumping)
    {
        if (isJumping)
            anim.SetBool("isDoubleJumping", true);
        else
            ToggleState("isJumping");
    }

    IEnumerator IdleBreak()
    {
        while (true)
        {
            if (anim.GetBool("isIdling") && Random.Range(0, 5) == 0)
            {
                anim.SetInteger("idleBreakNum", Random.Range(1, 2));
                anim.SetBool("isIdleBreak", true);
            }

            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            if (Random.Range(0, 5) == 0)
                anim.SetBool("isBlinking", true);

            yield return new WaitForSeconds(2);
        }
    }
}
