using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIDEARENA : MonoBehaviour
{
    public GuideBoss guide;
    bool combatStarted = false;

    void OnCollisionEnter(Collision col)
    {
        if (!combatStarted)
        {
            combatStarted = true;
            guide.StartCombat();
        }
    }
}
