using UnityEngine;

public class PlayerMotion : MonoBehaviour {

    public Animator anim;

    public float runSpeed;
    public float turnSpeed;
    public float jumpForce;
    
    public int jumpCount { get; set; }
    const int MAX_JUMP = 2;

    Rigidbody rigid;

    string vertical = Inputs.playerVAxis;
    string horizontal = Inputs.playerHAxis;
    string strafe = Inputs.playerStrafeAxis;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Input.GetAxis(strafe) * runSpeed * Time.deltaTime,
            0,
            Input.GetAxis(vertical) * runSpeed * Time.deltaTime);
        //transform.Rotate(0, Input.GetAxisRaw(horizontal) * turnSpeed, 0);

        if (Input.GetKeyDown(Inputs.jump) /*&& jumpCount < maxJump*/)
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(transform.up * jumpForce);
            jumpCount++;
        }

        if (Input.GetKey(Inputs.sprint))
            transform.Translate(0, 0, Input.GetAxis(vertical) * runSpeed * Time.deltaTime);

        if (Input.GetKey(Inputs.aim))
            anim.SetBool("isAiming", true);
        else if (Input.GetKeyUp(Inputs.aim))
            anim.SetBool("isAiming", false);
    }
}
