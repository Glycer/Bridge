﻿using System.Collections;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {

    public float currSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    // phasing protection
    bool horizontalMotionLocked;
    bool verticalMotionLocked;
    // dodge lock
    public bool motionLocked;

    //public float turnSpeed;
    public float jumpForce;
    
    public int jumpCount { get; set; }
    const int MAX_JUMP = 2;

    Rigidbody rigid;
    CharacterMotion characterMotion;
    CharacterAnimControl animControl;

    string vertical = Inputs.playerVAxis;
    string horizontal = Inputs.playerHAxis;
    string strafe = Inputs.playerStrafeAxis;

    public UIManager UI;

    // Use this for initialization
    void Start()
    {
        PlayerStats.UI = UI;
        rigid = GetComponent<Rigidbody>();
        characterMotion = GetComponent<CharacterMotion>();
        animControl = GetComponent<CharacterAnimControl>();
        horizontalMotionLocked = false;
        currSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!motionLocked)
        {
            Turn(Inputs.playerHAxis);

            if (!horizontalMotionLocked)
            {
                transform.Translate(Input.GetAxis(strafe) * currSpeed * Time.deltaTime,
                    0,
                    Input.GetAxis(vertical) * currSpeed * Time.deltaTime);
            }

            if (!verticalMotionLocked)
            {
                if (Input.GetKeyDown(Inputs.jump) /*&& jumpCount < MAX_JUMP*/)
                {
                    rigid.velocity = Vector3.zero;
                    rigid.AddForce(transform.up * jumpForce);

                    if (animControl.enabled)
                        animControl.Jump(!(jumpCount == 0));

                    jumpCount++;
                }
            }
        }
    }

    public void Turn(string axis)
    {
        transform.Rotate(0, Input.GetAxis(axis) * turnSpeed * Time.deltaTime, 0);
    }

    // Detects collisions to prevent phasing
    public void HorizontalCollision()
    {
        horizontalMotionLocked = true;
        StartCoroutine(horizontalBounceBack());
    }

    // Detects vertical collisions to prevent phasing
    public void VerticalCollision()
    {
        verticalMotionLocked = true;
        StartCoroutine(verticalBounceBack());
    }

    // Pushes player back to prevent phasing
    IEnumerator horizontalBounceBack()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.Translate(Input.GetAxis(strafe) * currSpeed * Time.deltaTime * -1,
            0,
            Input.GetAxis(vertical) * currSpeed * Time.deltaTime * -1);
            yield return new WaitForSeconds(.01f);
        }
        horizontalMotionLocked = false;
    }

    IEnumerator verticalBounceBack()
    {
        for (int i = 0; i < 5; i++)
        {
            // TODO: bounce player vertically
            yield return new WaitForSeconds(.01f);
        }
        verticalMotionLocked = false;
    }
}
