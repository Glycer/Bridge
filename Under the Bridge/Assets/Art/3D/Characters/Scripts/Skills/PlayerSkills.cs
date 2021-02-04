using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public Weapon[] weapons;
    protected int currActiveWeaponIndex;
    public PlayerMotion motion;
    public CharacterMotion charMotion;
    public UIManager UI;
    public GameObject HUD;
    public bool defending;
    public bool dodging;

    static Coroutine motionLock;
    static Coroutine rotationLock;

    // For rotation and motion locking
    const float DEFAULT_LOCK_TIME = 1;

    // For lock on player orientation
    public CamControl camControl;

    protected Ability[] abilities;
    KeyCode[] abilityArray = new KeyCode[] { Inputs.ability1, Inputs.ability2, Inputs.ability3, Inputs.ability4 };

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(Inputs.whack) || Input.GetKeyDown(Inputs.secondary)) && camControl.locked)
            TurnToFaceEnemy();
        if (Input.GetKeyDown(Inputs.whack))
            Whack(true);
        if (Input.GetKeyUp(Inputs.whack))
            Whack(false);
        if (Input.GetKeyDown(Inputs.secondary))
            Secondary(true);
        if (Input.GetKeyUp(Inputs.secondary))
            Secondary(false);
        if (Input.GetKeyDown(Inputs.defense))
            Defense(true);
        if (Input.GetKeyUp(Inputs.defense))
            Defense(false);
        for (int i = 0; i < abilityArray.Length; i++)
        {
            if (Input.GetKeyDown(abilityArray[i]))
                Ability(i, true);
            if (Input.GetKeyUp(abilityArray[i]))
                Ability(i, false);
        }

        if (Input.GetKeyDown(Inputs.sprint))
            Sprint(true);
        if (Input.GetKeyUp(Inputs.sprint))
            Sprint(false);
    }

    // Turns player when attacking and locked on
    protected virtual void TurnToFaceEnemy()
    {
        motion.FaceEnemy();
        LockCharacterRotation();
    }
    // Locks character rotation, overrides existing lock
    protected void LockCharacterRotation(float waitTime = DEFAULT_LOCK_TIME)
    {
        if (rotationLock != null)
            rotationLock = null;
        charMotion.rotationLocked = true;
        rotationLock = StartCoroutine(RotationLock(waitTime));
    }
    // Locks character rotation and player motion, overrides both locks
    public void LockPlayerMotion(float waitTime = DEFAULT_LOCK_TIME)
    {
        if (motionLock != null)
            motionLock = null;
        if (rotationLock != null)
            rotationLock = null;
        motion.motionLocked = true;
        charMotion.rotationLocked = true;
        rotationLock = StartCoroutine(RotationLock(waitTime));
        motionLock = StartCoroutine(MotionLock(waitTime));
    }
    protected IEnumerator RotationLock(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        charMotion.rotationLocked = false;
    }
    protected IEnumerator MotionLock(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        motion.motionLocked = false;
    }
    // Used when character is switched
    protected void ReleaseMotionLock()
    {
        if (motionLock != null)
            motionLock = null;
        if (rotationLock != null)
            rotationLock = null;
        motion.motionLocked = false;
        charMotion.rotationLocked = false;
    }

    protected virtual void Whack(bool keyDown)
    {
    }
    protected virtual void Secondary(bool keyDown)
    {
    }
    protected virtual void Defense(bool keyDown)
    {
    }
    void Ability(int abilityIndex, bool keyDown)
    {
        if (abilities[abilityIndex] != null && PlayerStats.CheckMana(abilities[abilityIndex].manaCost))
            abilities[abilityIndex].UseAbility(keyDown);
    }

    void Sprint(bool keyDown)
    {
        if (keyDown)
            motion.currSpeed = motion.runSpeed;
        else
            motion.currSpeed = motion.walkSpeed;
    }

    public void EnableChar()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] != null)
                UI.AdjustAbilityDisplay(i, abilities[i]);
            else
                UI.AdjustAbilityDisplay(i, null);
        }
        if (HUD != null)
            HUD.SetActive(true);
    }
}
