using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCam : MonoBehaviour
{
    CamControl camControl;
    Coroutine resetRot;

    private void Start()
    {
        camControl = GetComponent<CamControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(Inputs.centerCam))
        {
            resetRotation();
        }
    }

    void resetRotation()
    {
        float duration = .2f;

        if (resetRot != null)
            StopCoroutine(resetRot);

        resetRot = StartCoroutine(Interpolater.InterpolateLocalRotation(camControl.turn, Quaternion.Euler(Vector3.zero), duration));
    }
}
