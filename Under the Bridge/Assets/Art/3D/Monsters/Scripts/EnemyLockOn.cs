using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    public PlayerFound playerScan;
    public PlayerFound playerInterest;
    public EnemyMotion motion;
    bool foundPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scan());
    }

    IEnumerator Scan()
    {
        while (true)
        {
            if (playerScan.target != null && foundPlayer == false)
            {
                transform.LookAt(playerScan.target.transform.position);
                foundPlayer = true;
                if (!motion.isPursuing)
                {
                    motion.Pursue();
                }
            }
            else if (foundPlayer == true && playerInterest.target != null)
            {
                transform.LookAt(playerInterest.target.transform.position);
            }
            else if (foundPlayer == true && playerInterest.target == null)
            {
                foundPlayer = false;
                motion.Halt();
            }

            yield return new WaitForSeconds(1/30f);
        }
    }
}
