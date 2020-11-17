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
    }

    protected override void Whack(bool keyDown)
    {
        if (weapons[currActiveWeaponIndex].gameObject.activeSelf)
        {
            TargetCollider targeter = weapons[currActiveWeaponIndex].GetComponent<TargetCollider>();

            for (int i = 0; i < targeter.targets.Count; i++)
            {
                //Debug.Log("Hit!");

                //Deals damage. 'If' statement checks death
                if (targeter.targets[i].gameObject.GetComponent<MonsterStats>() != null)
                    PlayerStats.AddMana(2, 10);
                if (targeter.targets[i].gameObject.GetComponent<MonsterStats>().TakeDamage(stats.getDamage()))
                    targeter.targets.Remove(targeter.targets[i]);
                else if (weapons[currActiveWeaponIndex].currEnchant != null)
                {
                    // if checks if enchantment is spent
                    if (weapons[currActiveWeaponIndex].currEnchant.UseEnchantment(targeter.targets[i].gameObject.GetComponent<MonsterStats>()))
                        weapons[currActiveWeaponIndex].currEnchant = null;
                    if (!targeter.targets[i].gameObject.activeSelf)
                        targeter.targets.Remove(targeter.targets[i]);
                }
            }
        }
    }
    protected override void Secondary(bool keyDown)
    {
        if (Input.GetKeyDown(Inputs.secondary))
            aim.Aim(true);
        if (Input.GetKeyUp(Inputs.secondary))
            aim.Aim(false);
    }
    protected override void Defense(bool keyDown)
    {
        defending = keyDown;
        // TODO: currently is a placeholder for cover animation
        if (keyDown)
            transform.localScale /= 2;
        else
            transform.localScale *= 2;
    }

    private void OnDisable()
    {
        if (aim.isAiming)
            aim.Aim(false);
        if (Input.GetKey(Inputs.defense))
            transform.localScale *= 2;
        defending = false;
        HUD.SetActive(false);
    }
}