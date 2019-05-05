using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{
    CamControl camControl;
    
    Coroutine zoom;
    Coroutine shiftFocus;

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

        zoom = isLocal ? StartCoroutine(Interpolater.LocalInterpolate(camControl.offset, pos, duration))
            : StartCoroutine(Interpolater.GlobalInterpolate(camControl.offset, pos, duration));
    }

    public void ZoomTo(Vector3 pos, Vector3 targetPos)
    {
        float duration = .2f;

        if (zoom != null)
            StopCoroutine(zoom);
        if (shiftFocus != null)
            StopCoroutine(shiftFocus);

        zoom = StartCoroutine(Interpolater.LocalInterpolate(camControl.offset, pos, duration));
        shiftFocus = StartCoroutine(Interpolater.LocalInterpolate(camControl.focusTarget, targetPos, duration));
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
