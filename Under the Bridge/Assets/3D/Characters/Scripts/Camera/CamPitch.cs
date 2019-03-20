using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPitch : MonoBehaviour
{
    public float speed;

    string vertical = Inputs.camVAxis;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Input.GetAxis(vertical) * speed * Time.deltaTime, 0);
    }
}
