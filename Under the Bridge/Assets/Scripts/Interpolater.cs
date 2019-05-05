using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Interpolater
{
    const float DELAY = .025f;

    public static IEnumerator LocalInterpolate(Transform interpolatee, Vector3 endPoint, float duration)
    {
        Vector3 startPoint = interpolatee.localPosition;

        //This float goes from 0 to 1 in the for loop
        float interPose = 0;

        for (float i = 0; i <= duration; i += DELAY)
        {
            interPose = i / duration;
            
            interpolatee.localPosition = Vector3.Lerp(startPoint, endPoint, interPose);

            yield return new WaitForSeconds(DELAY);
        }
        
        interpolatee.localPosition = endPoint;
    }

    public static IEnumerator GlobalInterpolate(Transform interpolatee, Vector3 endPoint, float duration)
    {
        Vector3 startPoint = interpolatee.position;

        //This float goes from 0 to 1 in the for loop
        float interPose = 0;

        for (float i = 0; i <= duration; i += DELAY)
        {
            interPose = i / duration;

            interpolatee.position = Vector3.Lerp(startPoint, endPoint, interPose);

            yield return new WaitForSeconds(DELAY);
        }

        interpolatee.position = endPoint;
    }
}
