using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    //public Weapon[] weapons;
    protected int currActiveWeaponIndex;
    public PlayerMotion motion;
    public CharacterMotion charMotion;
    public UIManager UI;
    public GameObject HUD;
    //public bool defending;
    public bool dodging;
    public bool sprinting;

    bool usingSecondary;

    static Coroutine motionLock;
    static Coroutine rotationLock;

    // For rotation and motion locking
    const float DEFAULT_LOCK_TIME = 1;

    // For lock on player orientation
    public CamControl camControl;

    public Ability[] abilities;
    KeyCode[] abilityArray = new KeyCode[] { Inputs.ability1, Inputs.ability2, Inputs.ability3, Inputs.ability4 };
    protected SecondaryAttack[] secondaries;

    const int MAX_ABILITIES = 4;

    // Update is called once per frame
    void Update()
    {
        if (!PlayerMotion.MotionLocked())
        {
            if ((Input.GetKeyDown(Inputs.whack) || Input.GetKeyDown(Inputs.secondary)) && camControl.locked)
                TurnToFaceEnemy();
            if (Input.GetKeyDown(Inputs.whack))
                Attack(true);
            if (Input.GetKeyUp(Inputs.whack))
                Attack(false);
            // Use secondary
            if (usingSecondary)
            {
                for (int i = 0; i < abilityArray.Length; i++)
                {
                    if (Input.GetKeyDown(abilityArray[i]))
                        Secondary(i);
                }
            }
            // Use ability
            else
            {
                for (int i = 0; i < abilityArray.Length; i++)
                {
                    if (Input.GetKeyDown(abilityArray[i]))
                        Ability(i, true);
                    if (Input.GetKeyUp(abilityArray[i]))
                        Ability(i, false);
                }
            }
            // Switch weapon
            if (Input.GetKeyDown(Inputs.weapon1))
                SwitchWeapon(0);
            if (Input.GetKeyDown(Inputs.weapon2))
                SwitchWeapon(1);
            if (Input.GetKeyDown(Inputs.weapon3))
                SwitchWeapon(2);

            if (Input.GetKeyDown(Inputs.defense))
                Defense(true);
            if (Input.GetKeyDown(Inputs.sprint))
                Run(true);
        }
        if (Input.GetKeyUp(Inputs.defense))
            Defense(false);
        if (Input.GetKeyUp(Inputs.sprint))
            Run(false);

        if (Input.GetKeyDown(Inputs.secondary))
            DisplaySecondary(true);
        if (Input.GetKeyUp(Inputs.secondary))
            DisplaySecondary(false);
    }

    // Turns player when attacking and locked on
    protected virtual void TurnToFaceEnemy()
    {
        motion.FaceEnemy();
    }
    // Locks player motion
    public void LockPlayerMotion(float waitTime = DEFAULT_LOCK_TIME)
    {
        if (motionLock != null)
            motionLock = null;
        PlayerMotion.LockMotion(true);
        motionLock = StartCoroutine(MotionLock(waitTime));
    }
    protected IEnumerator MotionLock(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayerMotion.LockMotion(false);
    }
    // Used when character is switched
    protected void ReleaseMotionLock()
    {
        if (motionLock != null)
            motionLock = null;
        PlayerMotion.LockMotion(false);
    }

    protected virtual void Attack(bool keyDown)
    {
    }
    void DisplaySecondary(bool keyDown)
    {
        usingSecondary = keyDown;

        // Switch abilities to secondary abilities
        if (keyDown)
        {
            for (int i = 0; i < MAX_ABILITIES; i++)
            {
                if (i < secondaries.Length && secondaries[i] != null)
                    UI.AdjustAbilityDisplay(i, secondaries[i].abilityImage);
                else
                    UI.AdjustAbilityDisplay(i, null);
            }
        }
        // Switch to normal abilities
        else
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                if (abilities[i] != null)
                    UI.AdjustAbilityDisplay(i, abilities[i].abilityImage);
                else
                    UI.AdjustAbilityDisplay(i, null);
            }
        }
    }
    protected virtual void Secondary(int secondaryIndex)
    {
    }
    protected virtual void Defense(bool keyDown)
    {
    }
    void Ability(int abilityIndex, bool keyDown)
    {
        if (!PlayerMotion.MotionLocked() && abilities[abilityIndex] != null && PlayerStats.CheckMana(abilities[abilityIndex].manaCost))
            abilities[abilityIndex].UseAbility(keyDown);
    }

    protected virtual void SwitchWeapon(int weaponIndex)
    {
    }

    void Run(bool keyDown)
    {
        if (!sprinting)
        {
            if (keyDown)
                motion.currSpeed = motion.runSpeed;
            else
                motion.currSpeed = motion.walkSpeed;
        }
    }

    public void EnableChar()
    {
        usingSecondary = false;

        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] != null)
                UI.AdjustAbilityDisplay(i, abilities[i].abilityImage);
            else
                UI.AdjustAbilityDisplay(i, null);
        }
        if (HUD != null)
            HUD.SetActive(true);
    }
}
