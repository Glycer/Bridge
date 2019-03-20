using UnityEngine;

public class GroundHit : MonoBehaviour
{
    PlayerMotion player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMotion>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.jumpCount = 0;
    }
}
