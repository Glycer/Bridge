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
        StartCoroutine(Scan());
    }

    IEnumerator Scan()
    {
        while (true)
        {
            if (playerScan.target != null)
            {
                transform.LookAt(playerScan.target.transform.position);
                transform.Translate(0, 0, moveSpeed / 30);
            }

            yield return new WaitForSeconds(1/30f);
        }
    }
}
