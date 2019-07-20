using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControl : MonoBehaviour
{
    public float speed;
    public Vector3 jumpForce;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Input.GetAxis(Inputs.playerVAxis) * speed * Time.deltaTime);

        if (Input.GetKeyDown(Inputs.jump))
            GetComponent<Rigidbody>().AddRelativeForce(jumpForce);
    }
}
