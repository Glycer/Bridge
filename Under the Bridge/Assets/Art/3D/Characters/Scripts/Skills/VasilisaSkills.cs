using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaSkills : MonoBehaviour
{
    Animator charAnim;
    public Animator fxAnim;

    // Start is called before the first frame update
    void Start()
    {
        charAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Inputs.magic1))
        {
            charAnim.SetBool("isPushing", true);
            charAnim.SetInteger("idleBreakNum", 0);
            fxAnim.Play("WaterPush");
        }
    }
}
