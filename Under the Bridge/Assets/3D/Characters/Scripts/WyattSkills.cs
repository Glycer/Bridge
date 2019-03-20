using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WyattSkills : MonoBehaviour
{
    //public static UnityAction Aim;
    //public static UnityAction DeAim;

    public Transform player;
    public Animator anim;

    GameObject blinkShot;
    Rigidbody blinkRigid;

    Coroutine blinkShotTimer;

    // Start is called before the first frame update
    void Start()
    {
        blinkShot = transform.GetChild(0).gameObject;
        blinkRigid = blinkShot.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Inputs.mobility))
        {
            if (!blinkShot.activeSelf)
            {
                blinkShot.SetActive(true);
                blinkShot.transform.parent = null;

                blinkRigid.velocity = Vector3.zero;
                blinkRigid.AddForce((transform.forward * 10000) + (transform.up * 10));

                blinkShotTimer = StartCoroutine(BlinkShotTimer());
            }
            else
            {
                player.position = blinkShot.transform.position;

                BlinkShotDespawn();
                StopCoroutine(blinkShotTimer);
            }
        }

        if (Input.GetKey(Inputs.aim))
            anim.SetBool("isAiming", true);
        else if (Input.GetKeyUp(Inputs.aim))
            anim.SetBool("isAiming", false);
    }

    IEnumerator BlinkShotTimer()
    {
        yield return new WaitForSeconds(5);

        BlinkShotDespawn();
    }

    void BlinkShotDespawn()
    {
        blinkShot.SetActive(false);
        blinkShot.transform.parent = transform;
        blinkShot.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}