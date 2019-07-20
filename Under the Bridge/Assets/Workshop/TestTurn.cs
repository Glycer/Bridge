using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurn : MonoBehaviour
{
    public float turnSpeed;

    Vector3 up = new Vector3(0, 1, 0);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(up, Input.GetAxis(Inputs.playerHAxis) * turnSpeed * Time.deltaTime);
    }
}
