﻿using System.Collections;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {

    public float runSpeed;

    // phasing protection
    bool horizontalMotionLocked;
    bool verticalMotionLocked;

    //public float turnSpeed;
    public float jumpForce;
    
    public int jumpCount { get; set; }
    const int MAX_JUMP = 2;

    Rigidbody rigid;
    CharacterAnimControl animControl;

    string vertical = Inputs.playerVAxis;
    string horizontal = Inputs.playerHAxis;
    string strafe = Inputs.playerStrafeAxis;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        animControl = GetComponent<CharacterAnimControl>();
        horizontalMotionLocked = false;
    }

    // Update is called once per frame
    void Update() {
        if (!horizontalMotionLocked)
        {
            transform.Translate(Input.GetAxis(strafe) * runSpeed * Time.deltaTime,
                0,
                Input.GetAxis(vertical) * runSpeed * Time.deltaTime);

            if (Input.GetKey(Inputs.sprint))
                transform.Translate(0, 0, Input.GetAxis(vertical) * runSpeed * Time.deltaTime);
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

    // Detects collisions to prevent phasing
    public void horizontalCollision()
    {
        horizontalMotionLocked = true;
        StartCoroutine(horizontalBounceBack());
    }

    // Detects vertical collisions to prevent phasing
    public void verticalCollision()
    {
        verticalMotionLocked = true;
        StartCoroutine(verticalBounceBack());
    }

    // Pushes player back to prevent phasing
    IEnumerator horizontalBounceBack()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.Translate(Input.GetAxis(strafe) * runSpeed * Time.deltaTime * -1,
            0,
            Input.GetAxis(vertical) * runSpeed * Time.deltaTime * -1);
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
