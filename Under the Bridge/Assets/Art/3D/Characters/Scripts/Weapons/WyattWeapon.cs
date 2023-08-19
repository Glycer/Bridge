using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyattWeapon : Weapon
{
    public AmmoType[] ammoTypes;

    // Used for Wyatt's weapons
    public AmmoType defaultAmmo;
    public AmmoType currAmmo;

    public float speedPenalty;

    WyattSkills skills;

    int currAmmoCount;

    const float RELOAD_TIME_TICK = 0.02f;
    Coroutine reloadTimer;
    bool reloading;

    Coroutine holdDownFire;
    Coroutine shotDelay;
    bool awaitingDelay;

    //enum firingTypes { semiAuto, fullAuto, charged };

    public override void Attack(bool keyDown)
    {
        // Only go through if shot is ready to fire or key is released
        if ((!reloading && !awaitingDelay) || !keyDown)
        {
            skills.GetTargeter().RefreshList();

            switch (currAmmo.GetFiringType())
            {
                case 0:
                    SemiAutoAttack(keyDown);
                    break;
                case 1:
                    FullAutoAttack(keyDown);
                    break;
                case 2:
                    ChargedAttack(keyDown);
                    break;
            }
        }
    }

    public override void Secondary(int secondaryIndex)
    {
        // index 0 cancels reload
        if (!reloading || secondaryIndex == 0)
        {
            if (secondaryIndex == 0)
            {
                if (reloading)
                    CancelReload();
                else if (currAmmo != defaultAmmo)
                {
                    currAmmo = defaultAmmo;
                    currAmmoCount = 0;
                    skills.SetAmmoDisplay(currAmmoCount);
                }

            }
            else if (secondaryIndex < secondaries.Length)
            {
                if (reloadTimer != null)
                    reloadTimer = null;
                reloadTimer = StartCoroutine(ReloadTimer(secondaries[secondaryIndex].GetLockTime(), secondaryIndex));
            }
        }
    }

    IEnumerator ReloadTimer(float reloadTime, int secondaryIndex)
    {
        reloading = true;
        float timePassed = 0;
        while (timePassed < reloadTime)
        {
            yield return new WaitForSeconds(RELOAD_TIME_TICK);
            timePassed += RELOAD_TIME_TICK;
            skills.UI.SetReloadBar(timePassed / reloadTime);
        }
        skills.UI.SetReloadBar(0);
        currAmmo = ammoTypes[secondaryIndex];
        currAmmo.LoadAmmo();
        skills.SetAmmoDisplay(currAmmo.GetMaxAmmo());
        reloading = false;
    }
    public void CancelReload()
    {
        if (reloadTimer != null)
            StopCoroutine(reloadTimer);
        skills.UI.SetReloadBar(0);
        reloading = false;
    }

    void SemiAutoAttack(bool keyDown)
    {
        if (keyDown)
        {
            FireShot();
        }
    }
    void FullAutoAttack(bool keyDown)
    {
        if (!keyDown && holdDownFire != null)
        {
            StopCoroutine(holdDownFire);
            holdDownFire = null;
        }
        else if (keyDown)
        {
            if (holdDownFire != null)
                holdDownFire = null;
            holdDownFire = StartCoroutine(FireAutoShots());
        }
    }
    void ChargedAttack(bool keyDown)
    {
        if (!keyDown && holdDownFire != null)
        {
            StopCoroutine(holdDownFire);
            holdDownFire = null;
        }
        else if (keyDown)
        {
            if (holdDownFire != null)
                holdDownFire = null;
            holdDownFire = StartCoroutine(ChargeShot());
        }
    }

    IEnumerator FireAutoShots()
    {
        AmmoType firingAmmo = currAmmo;

        while (firingAmmo == currAmmo)
        {
            yield return new WaitForSeconds(firingAmmo.GetDelay());
            FireShot();
        }
    }
    IEnumerator ChargeShot()
    {
        yield return new WaitForSeconds(currAmmo.GetChargeTime());
        FireShot();
    }

    void FireShot()
    {
        for (int i = 0; i < skills.GetTargeter().targets.Count; i++)
        {
            // Deals damage. 'if' statement checks death
            if (skills.GetTargeter().targets[i].gameObject.GetComponent<MonsterStats>() != null)
                PlayerStats.AddMana(2, currAmmo.GetManaFill());
            if (skills.GetTargeter().targets[i].gameObject.GetComponent<MonsterStats>() != null && skills.GetTargeter().targets[i].gameObject.GetComponent<MonsterStats>().TakeDamage(currAmmo.GetStrength()))
                skills.GetTargeter().targets.Remove(skills.GetTargeter().targets[i]);
        }

        // Start shot delay
        if (shotDelay != null)
            shotDelay = null;
        shotDelay = StartCoroutine(ShotDelay(currAmmo.GetDelay()));

        GetComponent<AudioSource>().clip = currAmmo.GetAudioClip();
        GetComponent<AudioSource>().Play();

        if (currAmmo != defaultAmmo)
        {
            currAmmoCount = currAmmo.ConsumeAmmo();
            if (currAmmoCount == 0)
                currAmmo = defaultAmmo;
            skills.SetAmmoDisplay(currAmmoCount);
        }
    }

    IEnumerator ShotDelay(float delay)
    {
        awaitingDelay = true;
        PlayerMotion.AddSpeedPenalty("Wyatt", speedPenalty);
        yield return new WaitForSeconds(delay);
        awaitingDelay = false;
        PlayerMotion.RemoveSpeedPenalty("Wyatt");
    }

    // Initialize
    public void SetSkills(WyattSkills newSkills)
    {
        currAmmo = ammoTypes[0];
        reloading = false;
        awaitingDelay = false;
        skills = newSkills;
    }

    private void OnDisable()
    {
        GetComponent<AudioSource>().clip = null;
        CancelReload();
        if (holdDownFire != null)
            StopCoroutine(holdDownFire);
        if (shotDelay != null)
            shotDelay = null;
        PlayerMotion.RemoveSpeedPenalty("Wyatt");
        awaitingDelay = false;
    }


    // placeholder
    public void adjustPitch(float adjustValue)
    {
        transform.Rotate(adjustValue * 8, 0, 0);
    }
}
