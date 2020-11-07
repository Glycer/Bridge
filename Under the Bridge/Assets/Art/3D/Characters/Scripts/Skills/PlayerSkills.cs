using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public PlayerMotion motion;
    public bool defending;
    public bool dodging;

    protected Ability[] abilities;
    KeyCode[] abilityArray = new KeyCode[] { Inputs.ability1, Inputs.ability2, Inputs.ability3, Inputs.ability4 };

    // Update is called once per frame
    void Update()
    {
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
        if (abilities[abilityIndex] != null)
            abilities[abilityIndex].UseAbility(keyDown);
    }

    void Sprint(bool keyDown)
    {
        if (keyDown)
            motion.currSpeed = motion.runSpeed;
        else
            motion.currSpeed = motion.walkSpeed;
    }
}
