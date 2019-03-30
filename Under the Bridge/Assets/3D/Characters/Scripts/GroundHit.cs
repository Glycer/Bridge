using UnityEngine;

public class GroundHit : MonoBehaviour
{
    PlayerMotion player;
    CharacterAnimControl characterAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMotion>();
        characterAnim = GetComponentInParent<CharacterAnimControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.jumpCount = 0;
        characterAnim.anim.SetBool("isJumping", false);
    }
}
