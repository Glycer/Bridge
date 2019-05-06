using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{
    CamControl camControl;
    
    Coroutine zoom;
    Coroutine shiftFocus;
    Coroutine resetRot;

    Coroutine lookAt;

    private void Start()
    {
        camControl = GetComponent<CamControl>();

        //StartCoroutine(LoS());
    }

    public void ZoomTo(Vector3 pos, bool isLocal)
    {
        float duration = .2f;

        if (zoom != null)
            StopCoroutine(zoom);

        zoom = isLocal ? StartCoroutine(Interpolater.InterpolateLocalTransform(camControl.offset, pos, duration))
            : StartCoroutine(Interpolater.InterpolateGlobalTransform(camControl.offset, pos, duration));
    }

    public void ZoomTo(Vector3 pos, Vector3 targetPos)
    {
        float duration = .2f;

        if (zoom != null)
            StopCoroutine(zoom);
        if (shiftFocus != null)
            StopCoroutine(shiftFocus);
        if (resetRot != null)
            StopCoroutine(resetRot);

        zoom = StartCoroutine(Interpolater.InterpolateLocalTransform(camControl.offset, pos, duration));
        shiftFocus = StartCoroutine(Interpolater.InterpolateLocalTransform(camControl.focusTarget, targetPos, duration));
        resetRot = StartCoroutine(Interpolater.InterpolateLocalRotation(camControl.turn, Quaternion.Euler(Vector3.zero), duration));
    }

    public void ToggleLook(bool isAiming)
    {
        float duration = .2f;

        if (lookAt != null)
            StopCoroutine(lookAt);

        lookAt = StartCoroutine(Interpolater.InterpolateConstraintWeight(camControl.camLook, isAiming ? 0 : 1, duration));
    }

    IEnumerator LoS()
    {
        Ray ray = new Ray(camControl.offset.position, transform.forward);
        RaycastHit hit;

        while (true)
        {
            Physics.Raycast(ray, out hit);

            if (hit.transform.name != "Player")
                ZoomTo(hit.point, false);

            yield return new WaitForSeconds(.2f);
        }
    }
}
