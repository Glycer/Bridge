using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WyattSkills : MonoBehaviour
{
    //public static UnityAction Aim;
    //public static UnityAction DeAim;

    public Transform player;

    public GameObject pistol;
    public GameObject blinkShot;
    Rigidbody blinkRigid;

    Coroutine blinkShotTimer;

    // Start is called before the first frame update
    void Start()
    {
        blinkRigid = blinkShot.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Watches for ability call
        if (Input.GetKeyDown(Inputs.mobility))
        {
            if (!blinkShot.activeSelf) // Checks for active shot
            {
                // Activates and unbinds
                blinkShot.SetActive(true);
                blinkShot.transform.parent = null;

                blinkRigid.velocity = Vector3.zero;
                blinkRigid.AddForce((transform.forward * 3000) + (transform.up * 1000));

                blinkShotTimer = StartCoroutine(BlinkShotTimer());
            }
            else // Blinks
            {
                player.position = blinkShot.transform.position;

                BlinkShotDespawn();
                StopCoroutine(blinkShotTimer);
            }
        }

        if (Input.GetKeyDown(Inputs.whack) && pistol.activeSelf)
        {
            foreach (Collider col in pistol.GetComponent<TargetCollider>().targets)
            {
                Debug.Log("Hit!");
                col.gameObject.GetComponent<MonsterStats>().TakeDamage(1);
            }

        }
    }

    // Shot times out after some time
    IEnumerator BlinkShotTimer()
    {
        yield return new WaitForSeconds(5);

        BlinkShotDespawn();
    }

    // Resets shot
    void BlinkShotDespawn()
    {
        blinkShot.SetActive(false);
        blinkShot.transform.parent = transform;
        blinkShot.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}