using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour {

    public UnityAction<Collider> Port;
    public UnityAction<Maze> SetCams;

    public Portal destination;
    public bool isReceiving = false;

    //The level that this portal is connected to.
    public Maze connectedLevel;

    private void Start()
    {
        Port += Send;
        SetCams += SetCamsPlaceholder;
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
            destination.isReceiving = true;
            //Get where the player is relative to the portal and place them in the same relative position on the other side.
            Transform player = other.transform;

            Vector3 relativePos = transform.InverseTransformPoint(player.position);
            relativePos.z = 0;
            //Vector3 direction = transform.InverseTransformDirection(player.forward);
            //direction = direction * -1;

            Transform parent = player.parent;
            player.parent = destination.transform;
            player.localPosition = relativePos;
            //player.forward = direction; //This is global forward, not local
            player.parent = parent;

            //Position works, commented out rotation is buggy.
            player.rotation = destination.transform.rotation;

            SetCams(connectedLevel);
        }
    }

    void OnTriggerExit()
    {
        isReceiving = false;
    }

    void SetCamsPlaceholder(Maze maze)
    { }
}