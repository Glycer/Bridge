using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayerHorizontal : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        col.gameObject.GetComponent<PlayerMotion>().horizontalCollision();
    }
}
