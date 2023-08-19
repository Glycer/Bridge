using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaAttack : SecondaryAttack
{
    public Transform attackPosition;

    // VasilisaFocus items
    public float verticalOffset;

    public void SetAttackPosition(Vector3 newPosition)
    {
        attackPosition.position = newPosition + new Vector3(0, verticalOffset, 0);
    }
    public float GetVerticalOffset()
    {
        return verticalOffset;
    }
}
