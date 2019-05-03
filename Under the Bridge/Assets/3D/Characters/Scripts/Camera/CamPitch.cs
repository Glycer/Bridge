using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPitch : MonoBehaviour
{
    public float speed;
    public WyattHandgun handgun;

    string vertical = Inputs.camVAxis;

    public bool pitchIsLocked = false;
    bool lookUpLocked = false;
    bool lookDownLocked = false;

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.x > 75 && transform.rotation.eulerAngles.x < 90)
            lookUpLocked = true;
        if (transform.rotation.eulerAngles.x > 270 && transform.rotation.eulerAngles.x < 285)
            lookDownLocked = true;
        if (!pitchIsLocked)
        {
            if (lookUpLocked && Input.GetAxis(vertical) < 0)
                lookUpLocked = false;
            else if (lookDownLocked && Input.GetAxis(vertical) > 0)
                lookDownLocked = false;
            if (!lookDownLocked && !lookUpLocked)
            {
                transform.Translate(0, Input.GetAxis(vertical) * speed * Time.deltaTime, 0);
                if (Input.GetAxis(vertical) != 0)
                {
                    handgun.adjustPitch(Input.GetAxis(vertical));
                }
            }
        }
    }
}
