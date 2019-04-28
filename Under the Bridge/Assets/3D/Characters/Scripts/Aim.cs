using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    public Animator anim;
    public Image crosshair;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Inputs.aim))
            SetAim(true);
        else if (Input.GetKeyUp(Inputs.aim))
            SetAim(false);
    }

    void SetAim(bool isAiming)
    {
        anim.SetBool("isAiming", isAiming);
        crosshair.gameObject.SetActive(isAiming);
    }
}
