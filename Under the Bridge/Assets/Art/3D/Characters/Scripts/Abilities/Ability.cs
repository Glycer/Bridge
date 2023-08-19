using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public Sprite abilityImage;
    public int[] manaCost;

    public virtual void UseAbility(bool keyDown)
    {
    }
}
