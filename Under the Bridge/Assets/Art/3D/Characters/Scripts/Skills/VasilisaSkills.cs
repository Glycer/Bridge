using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaSkills : PlayerSkills
{
    // TODO: Temp code used until runtime ability setting is implemented
    public Ability waterPush;
    // Start is called before the first frame update
    void Awake()
    {
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
        if (keyDown && motion.currSpeed == motion.runSpeed)
            motion.currSpeed *= 2;
        else if (!keyDown && motion.currSpeed == motion.runSpeed * 2)
            motion.currSpeed = motion.runSpeed;
    }

    private void OnDisable()
    {
        if (motion.currSpeed == motion.runSpeed * 2)
            motion.currSpeed = motion.runSpeed;
    }
}
