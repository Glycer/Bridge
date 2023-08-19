using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaSkills : PlayerSkills
{
    public VasilisaWeapon[] weapons;
    // TODO: Temp code used until runtime ability setting is implemented
    public Ability waterPush;
    public SecondaryAttack magicMissile;
    public SecondaryAttack fireball;
    public SecondaryAttack fireblast;
    public SecondaryAttack arcaneRay;
    public float sprintSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        sprintSpeed = motion.runSpeed * 1.5f;
        sprinting = false;
        currActiveWeaponIndex = 0;
        secondaries = weapons[currActiveWeaponIndex].GetSecondaries();
    }

    protected override void Attack(bool keyDown)
    {
        weapons[currActiveWeaponIndex].Attack(keyDown);
    }
    protected override void Secondary(int secondaryIndex)
    {
        weapons[currActiveWeaponIndex].Secondary(secondaryIndex);
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

    protected override void SwitchWeapon(int weaponIndex)
    {
        if (!weapons[currActiveWeaponIndex].AttackLocked())
        {
            weapons[currActiveWeaponIndex].gameObject.SetActive(false);
            currActiveWeaponIndex = weaponIndex;
            weapons[currActiveWeaponIndex].gameObject.SetActive(true);
            secondaries = weapons[currActiveWeaponIndex].GetSecondaries();
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
    }
}
