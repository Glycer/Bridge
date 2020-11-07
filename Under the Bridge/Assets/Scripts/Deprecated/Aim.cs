using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    public Animator anim;
    public Image crosshair;

    public void SetAim(bool isAiming)
    {
        if (isAiming)
            anim.enabled = true;

        anim.SetBool("isAiming", isAiming);
        crosshair.gameObject.SetActive(isAiming);
    }
}
