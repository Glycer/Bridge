using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public static class Interpolater
{
    const float DELAY = .025f;

    public static IEnumerator InterpolateLocalTransform(Transform interpolatee, Vector3 endPoint, float duration)
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

    public static IEnumerator InterpolateGlobalTransform(Transform interpolatee, Vector3 endPoint, float duration)
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

    public static IEnumerator InterpolateLocalRotation(Transform interpolatee, Quaternion endPoint, float duration)
    {
        Quaternion startPoint = interpolatee.localRotation;

        //This float goes from 0 to 1 in the for loop
        float interPose = 0;

        for (float i = 0; i <= duration; i += DELAY)
        {
            interPose = i / duration;

            interpolatee.localRotation = Quaternion.Slerp(startPoint, endPoint, interPose);

            yield return new WaitForSeconds(DELAY);
        }

        interpolatee.localRotation = endPoint;
    }

    public static IEnumerator InterpolateConstraintWeight(IConstraint interpolatee, float endPoint, float duration)
    {
        float startPoint = interpolatee.weight;

        //This float goes from 0 to 1 in the for loop
        float interPose = 0;

        for (float i = 0; i <= duration; i += DELAY)
        {
            interPose = i / duration;

            interpolatee.weight = Mathf.Lerp(startPoint, endPoint, interPose);

            yield return new WaitForSeconds(DELAY);
        }

        interpolatee.weight = endPoint;
    }
}
