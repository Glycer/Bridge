using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayerHorizontal : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 10)
            col.gameObject.GetComponent<PlayerMotion>().HorizontalCollision();
        else if (col.gameObject.layer == 15 && col.gameObject.GetComponent<EnemyMotion>() != null)
            col.gameObject.GetComponent<EnemyMotion>().HorizontalCollision();
    }
}
