using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public Sprite abilityImage;
    public int[] manaCost;

    // Used by enchantment to add enchantment to weapon
    public Weapon weapon;

    public virtual void UseAbility(bool keyDown)
    {
    }
}
