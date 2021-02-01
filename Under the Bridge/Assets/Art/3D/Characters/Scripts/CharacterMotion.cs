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

    // Rotates character when character moves
    public void RotateCharacter()
    {
        float vAxis = Input.GetAxis(Inputs.playerVAxis);
        float hAxis = Input.GetAxis(Inputs.playerHAxis);

        float num = 0;
        if (vAxis > 0)
        {
            num = 0;
            if (hAxis > 0)
                num += (hAxis - vAxis + 1) * 45;
            else if (hAxis < 0)
                num -= (-hAxis - vAxis + 1) * 45;
        }
        else if (vAxis < 0)
        {
            num = 180;
            if (hAxis > 0)
                num -= (hAxis + vAxis + 1) * 45;
            else if (hAxis < 0)
                num += (-hAxis + vAxis + 1) * 45;
        }
        else
        {
            if (hAxis > 0)
                num = 90;
            else if (hAxis < 0)
                num = 270;
        }

        transform.localRotation = Quaternion.Euler(0, num, 0);
    }
    public void ResetRotation()
    {
        transform.localRotation = Quaternion.identity;
    }
}
