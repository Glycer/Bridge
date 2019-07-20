using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotion : MonoBehaviour
{
    public Transform head;
    public float speed;

    public void LookHorizontal(string inputAxis)
    {
        head.Rotate(0, Input.GetAxis(inputAxis) * speed * Time.deltaTime, 0);
    }

    public void LookVertical(string inputAxis)
    {
        head.Rotate(Input.GetAxis(inputAxis) * speed * Time.deltaTime, 0, 0);
    }
}
