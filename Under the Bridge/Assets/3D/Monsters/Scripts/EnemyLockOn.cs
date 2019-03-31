using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    public PlayerFound playerScan;
    public PlayerMotion player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerScan.foundTarget == true)
        {
            transform.LookAt(player.transform.position);
            transform.Translate(0, 0, 1);
        }
    }
}
