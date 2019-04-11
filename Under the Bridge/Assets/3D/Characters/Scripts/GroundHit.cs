using UnityEngine;

public class GroundHit : MonoBehaviour
{
    public GameObject player;

    PlayerMotion playerMotion;
    CharacterAnimControl characterAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerMotion = player.GetComponent<PlayerMotion>();
        characterAnim = player.GetComponent<CharacterAnimControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerMotion.jumpCount = 0;
        if (characterAnim.enabled)
            characterAnim.anim.SetBool("isJumping", false);
    }
}
