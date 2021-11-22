using System.Collections;
using System;
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

    public UIManager UI;

    public CamControl camControl;

    // Used by Update for keyboard motion
    float horizontalDirection;
    float verticalDirection;

    // Use this for initialization
    void Start()
    {
        PlayerStats.UI = UI;
        rigid = GetComponent<Rigidbody>();
        characterMotion = GetComponentInChildren<CharacterMotion>();
        animControl = GetComponent<CharacterAnimControl>();
        horizontalMotionLocked = false;
        currSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!motionLocked)
        {
            if (!horizontalMotionLocked)
            {
                if (Input.GetAxis(horizontal) != 0 || Input.GetAxis(vertical) != 0)
                    SetDirection();

                // Prevents axes from being added together
                horizontalDirection = Input.GetAxis(horizontal);
                verticalDirection = Input.GetAxis(vertical);
                horizontalDirection /= Math.Abs(Input.GetAxis(vertical)) + 1;
                verticalDirection /= Math.Abs(Input.GetAxis(horizontal)) + 1;

                transform.Translate(horizontalDirection * currSpeed * Time.deltaTime,
                    0,
                    verticalDirection * currSpeed * Time.deltaTime);
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

    public void SetDirection()
    {
        transform.localRotation *= Quaternion.Euler(0, camControl.turn.localRotation.eulerAngles.y, 0);
        camControl.turn.localRotation = Quaternion.Euler(camControl.turn.localRotation.eulerAngles.x, 0, camControl.turn.localRotation.eulerAngles.z);
    }

    public void Turn(string axis, float turnMultiplier = 1)
    {
        transform.Rotate(0, Input.GetAxis(axis) * turnSpeed * Time.deltaTime * turnMultiplier, 0);
    }

    // Used by portals
    public void OrientPlayer(Quaternion newRotation)
    {
        transform.localRotation = newRotation * Quaternion.Inverse(characterMotion.transform.localRotation);
    }
    // Used for lock on attack
    public void FaceEnemy()
    {
        characterMotion.SetRotation(camControl.focusTarget.localRotation.eulerAngles.y);
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
            transform.Translate(Input.GetAxis(horizontal) * currSpeed * Time.deltaTime * -1,
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
