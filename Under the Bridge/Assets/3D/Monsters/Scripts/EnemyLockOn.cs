using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    public PlayerFound playerScan;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scan());
    }

    IEnumerator Scan()
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
    }
}
