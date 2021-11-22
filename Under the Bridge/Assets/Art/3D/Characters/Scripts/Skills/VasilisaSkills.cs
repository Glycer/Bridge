using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaSkills : PlayerSkills
{
    // TODO: Temp code used until runtime ability setting is implemented
    public Ability waterPush;
    public float sprintSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        sprintSpeed = motion.runSpeed * 1.5f;
        sprinting = false;
        currActiveWeaponIndex = 0;
        abilities = new Ability[4];
        abilities[0] = waterPush;
    }

    protected override void Whack(bool keyDown)
    {
        if (keyDown)
            weapons[currActiveWeaponIndex].Primary();
    }
    protected override void Secondary(bool keyDown)
    {
        if (keyDown)
            weapons[currActiveWeaponIndex].Secondary();
    }
    protected override void Defense(bool keyDown)
    {
        if (keyDown && motion.currSpeed <= motion.runSpeed)
        {
            motion.currSpeed = sprintSpeed;
            sprinting = true;
        }
        else if (!keyDown && motion.currSpeed == sprintSpeed)
        {
            if (Input.GetKey(Inputs.sprint))
                motion.currSpeed = motion.runSpeed;
            else
                motion.currSpeed = motion.walkSpeed;
            sprinting = false;
        }
    }

    private void OnDisable()
    {
        if (sprinting)
        {
            if (Input.GetKey(Inputs.sprint))
                motion.currSpeed = motion.runSpeed;
            else
                motion.currSpeed = motion.walkSpeed;
            sprinting = false;
        }
        ReleaseMotionLock();
    }
}
