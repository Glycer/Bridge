using System.Collections;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {

    public float runSpeed;

    // for acceleration purposes
    private float currentSpeed;
    // determines rate of acceleration (number of frames before max speed)
    public float accelerationFactor;
    // determines speed before acceleration
    const int STARTING_SPEED_FACTOR = 10;

    // part of phasing protection
    bool horizontalMotionLocked;

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
        resetSpeed();
        horizontalMotionLocked = false;
    }

    // Update is called once per frame
    void Update() {
        if (!horizontalMotionLocked)
        {
            transform.Translate(Input.GetAxis(strafe) * currentSpeed * Time.deltaTime,
                0,
                Input.GetAxis(vertical) * currentSpeed * Time.deltaTime);

            if (Input.GetKey(Inputs.sprint))
                transform.Translate(0, 0, Input.GetAxis(vertical) * currentSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(Inputs.jump) /*&& jumpCount < MAX_JUMP*/)
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(transform.up * jumpForce);

            if (animControl.enabled)
                animControl.Jump(!(jumpCount == 0));

            jumpCount++;
        }

        if (currentSpeed < runSpeed && (Input.GetAxis(strafe) != 0 || Input.GetAxis(vertical) != 0))
            currentSpeed += runSpeed / accelerationFactor;
        if (currentSpeed > runSpeed)
            currentSpeed = runSpeed;

        if (Input.GetAxis(strafe) == 0 && Input.GetAxis(vertical) == 0 && currentSpeed > (runSpeed / STARTING_SPEED_FACTOR + 0.01))
            resetSpeed();
    }

    // Detects collisions to prevent phasing
    void OnCollisionEnter()
    {
        resetSpeed();
        horizontalMotionLocked = true;
        StartCoroutine(bounceBack());
        Debug.Log("Collision");
    }

    // Resets speed to prevent phasing
    void resetSpeed()
    {
        currentSpeed = runSpeed / STARTING_SPEED_FACTOR;
    }

    // Pushes player back to prevent phasing
    IEnumerator bounceBack()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.Translate(Input.GetAxis(strafe) * currentSpeed * Time.deltaTime * -1,
            0,
            Input.GetAxis(vertical) * currentSpeed * Time.deltaTime * -1);
            yield return new WaitForSeconds(.01f);
        }
        horizontalMotionLocked = false;
    }
}
