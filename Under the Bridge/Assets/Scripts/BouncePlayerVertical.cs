using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayerVertical : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        col.gameObject.GetComponent<PlayerMotion>().VerticalCollision();
    }
}
