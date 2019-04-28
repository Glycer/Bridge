using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    public PlayerFound playerScan;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = GetComponent<MonsterStats>().moveSpeed;
        //StartCoroutine(Scan());
    }

    void Update()
    {
        if (playerScan.target != null)
        {
            transform.LookAt(playerScan.target.transform.position);
            transform.Translate(0, 0, moveSpeed / 60);
        }
    }

    /*IEnumerator Scan()
    {
        while (true)
        {
            if (playerScan.target != null)
            {
                transform.LookAt(playerScan.target.transform.position);
                transform.Translate(0, 0, .1f);
            }

            yield return new WaitForSeconds(.1f);
        }
    }*/
}
