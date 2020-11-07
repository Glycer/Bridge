using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPush : Ability
{
    public Animator charAnim;
    public Animator fxAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UseAbility(bool KeyDown)
    {
        charAnim.SetBool("isPushing", true);
        charAnim.SetInteger("idleBreakNum", 0);
        fxAnim.Play("WaterPush");
    }
}
