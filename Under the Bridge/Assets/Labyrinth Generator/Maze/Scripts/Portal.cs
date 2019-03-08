using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public Action<Collider> Port;

    public Transform destination;
    public bool isReceiving = false;

    private void Start()
    {
        Port += Send;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isReceiving)
            Port(other);
    }

    void Send(Collider other)
    {
        if (destination != null)
        {
            destination.GetComponent<Portal>().isReceiving = true;
            other.transform.position = destination.transform.position;
            other.transform.rotation = destination.transform.rotation;
            //Debug.Log("Sent to " + destination.name);
        }
    }

    void OnTriggerExit()
    {
        isReceiving = false;
    }
}