using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkShot : Ability
{
    public Transform player;
    public GameObject blinkShot;
    Rigidbody blinkRigid;

    Coroutine blinkShotTimer;

    // Start is called before the first frame update
    void Start()
    {
        blinkRigid = blinkShot.GetComponent<Rigidbody>();
    }

    public override void UseAbility(bool keyDown)
    {
        if (!blinkShot.activeSelf && keyDown) // Checks for active shot
        {
            // Activates and unbinds
            blinkShot.SetActive(true);
            blinkShot.transform.parent = null;

            blinkRigid.velocity = Vector3.zero;
            blinkRigid.AddForce((transform.forward * 3000) + (transform.up * 1000));

            blinkShotTimer = StartCoroutine(BlinkShotTimer());
        }
        else if (keyDown) // Blinks
        {
            player.position = blinkShot.transform.position;

            BlinkShotDespawn();
            StopCoroutine(blinkShotTimer);

            PlayerStats.SpendMana(manaCost);
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
