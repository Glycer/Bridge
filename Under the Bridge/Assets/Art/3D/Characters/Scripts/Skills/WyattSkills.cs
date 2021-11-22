using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WyattSkills : PlayerSkills
{
    //public static UnityAction Aim;
    //public static UnityAction DeAim;

    public Transform player;

    public WyattStats stats;

    public CamControl aim;

    bool aiming;
    // TEMP DEFENSE DISPLAY, TODO: remove
    public GameObject cover;

    // TODO: Temp code used until runtime ability setting is implemented
    public Ability blinkShot;
    public Ability damageEnchant;
    // Start is called before the first frame update
    void Awake()
    {
        currActiveWeaponIndex = 0;
        abilities = new Ability[4];
        abilities[0] = blinkShot;
        abilities[1] = damageEnchant;
        aiming = false;
    }

    protected override void Whack(bool keyDown)
    {
        // Cannot attack while blocking
        if (weapons[currActiveWeaponIndex].gameObject.activeSelf && keyDown && !PlayerStats.defending)
        {
            TargetCollider targeter = weapons[currActiveWeaponIndex].GetComponentInChildren<TargetCollider>();

            targeter.RefreshList();
            for (int i = 0; i < targeter.targets.Count; i++)
            {
                // Deals damage. 'if' statement checks death
                if (targeter.targets[i].gameObject.GetComponent<MonsterStats>() != null)
                    PlayerStats.AddMana(2, 10);
                if (targeter.targets[i].gameObject.GetComponent<MonsterStats>().TakeDamage(stats.GetDamage()))
                    targeter.targets.Remove(targeter.targets[i]);
                else if (weapons[currActiveWeaponIndex].currEnchant != null)
                {
                    weapons[currActiveWeaponIndex].currEnchant.UseEnchantment(targeter.targets[i].gameObject.GetComponent<MonsterStats>());
                    if (!targeter.targets[i].gameObject.activeSelf)
                        targeter.targets.Remove(targeter.targets[i]);
                }
            }

            if (weapons[currActiveWeaponIndex].currEnchant != null && weapons[currActiveWeaponIndex].currEnchant.ConsumeCharge())
                weapons[currActiveWeaponIndex].currEnchant = null;
        }
    }
    protected override void Secondary(bool keyDown)
    {
        // Cannot aim while blocking
        if (Input.GetKeyDown(Inputs.secondary) && !PlayerStats.defending)
        {
            aim.Aim(true, weapons[currActiveWeaponIndex].GetComponentInChildren<TargetCollider>().GetComponent<CapsuleCollider>());
            aiming = true;
        }
        if (Input.GetKeyUp(Inputs.secondary) && aiming)
        {
            aim.Aim(false);
            aiming = false;
        }
    }
    protected override void Defense(bool keyDown)
    {
        // Cannot cover while aiming
        // TODO: currently is a placeholder for cover animation
        if (keyDown && !Input.GetKey(Inputs.secondary))
        {
            PlayerStats.defending = true;
            cover.SetActive(true);
        }
        else
        {
            PlayerStats.defending = false;
            cover.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (aim.isAiming)
            aim.Aim(false);
        // Decover if covering
        if (Input.GetKey(Inputs.defense))
            cover.SetActive(false);
        PlayerStats.defending = false;
        if (HUD != null)
            HUD.SetActive(false);
        ReleaseMotionLock();
    }
}