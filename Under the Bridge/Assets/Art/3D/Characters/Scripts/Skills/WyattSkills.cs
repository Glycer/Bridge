using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WyattSkills : PlayerSkills
{
    public TargetCollider targeter;
    public WyattWeapon[] weapons;

    public Transform player;

    public WyattStats stats;

    // TEMP DEFENSE DISPLAY, TODO: remove
    public GameObject cover;

    // TODO: Temp code used until runtime ability setting is implemented
    public Ability blinkShot;

    // Start is called before the first frame update
    void Awake()
    {
        currActiveWeaponIndex = 0;
        secondaries = new SecondaryAttack[4];
        secondaries = weapons[currActiveWeaponIndex].GetSecondaries();
        foreach (WyattWeapon weapon in weapons)
        {
            weapon.SetSkills(this);
        }
    }

    protected override void Attack(bool keyDown)
    {
        // Cannot attack while blocking
        if (weapons[currActiveWeaponIndex].gameObject.activeSelf && !PlayerStats.defending)
        {
            weapons[currActiveWeaponIndex].Attack(keyDown);
        }
    }
    protected override void Secondary(int secondaryIndex)
    {
        weapons[currActiveWeaponIndex].Secondary(secondaryIndex);
    }
    protected override void Defense(bool keyDown)
    {
        // TODO: currently is a placeholder for cover animation
        if (keyDown && !PlayerMotion.MotionLocked() && !Input.GetKey(Inputs.secondary))
        {
            PlayerStats.defending = true;
            cover.SetActive(true);
            PlayerMotion.LockMotion(true);
        }
        else
        {
            PlayerStats.defending = false;
            cover.SetActive(false);
            PlayerMotion.LockMotion(false);
        }
    }

    public TargetCollider GetTargeter()
    {
        return targeter;
    }
    public void SetAmmoDisplay(int ammoCount)
    {
        UI.SetAmmoDisplay(ammoCount);
    }

    protected override void SwitchWeapon(int weaponIndex)
    {
        weapons[currActiveWeaponIndex].gameObject.SetActive(false);
        currActiveWeaponIndex = weaponIndex;
        weapons[currActiveWeaponIndex].gameObject.SetActive(true);
        secondaries = weapons[currActiveWeaponIndex].GetSecondaries();
    }

    private void OnDisable()
    {
        weapons[currActiveWeaponIndex].CancelReload();
        // Decover if covering
        if (Input.GetKey(Inputs.defense))
            cover.SetActive(false);
        PlayerStats.defending = false;
        if (HUD != null)
            HUD.SetActive(false);
    }
}