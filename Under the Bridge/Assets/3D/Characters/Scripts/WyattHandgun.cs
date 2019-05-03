using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyattHandgun : MonoBehaviour
{
    // placeholder
    public void adjustPitch(float adjustValue)
    {
        transform.Rotate(adjustValue * 8, 0, 0);
    }
}
