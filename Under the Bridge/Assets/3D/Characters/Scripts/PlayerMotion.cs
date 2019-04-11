using UnityEngine;

public class PlayerMotion : MonoBehaviour {

    public Animator anim;

    public float runSpeed;
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
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Input.GetAxis(strafe) * runSpeed * Time.deltaTime,
            0,
            Input.GetAxis(vertical) * runSpeed * Time.deltaTime);

        if (Input.GetKeyDown(Inputs.jump) /*&& jumpCount < MAX_JUMP*/)
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(transform.up * jumpForce);

            if (animControl.enabled)
                animControl.Jump(!(jumpCount == 0));

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
