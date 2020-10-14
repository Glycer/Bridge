using UnityEngine;

public class StopMotion : MonoBehaviour
{
    public Transform player;
    Rigidbody rigid;

    private void Start()
    {
        rigid = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 pos = player.position;

        rigid.velocity = Vector3.zero;
        player.position = pos;
    }
}
