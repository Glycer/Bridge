using System;
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
            other.transform.position = destination.position;
            other.transform.rotation = destination.rotation;
            //Debug.Log(destination.transform.eulerAngles + " : " + other.transform.eulerAngles);
        }
    }

    void OnTriggerExit()
    {
        isReceiving = false;
    }
}