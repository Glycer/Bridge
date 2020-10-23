using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimControl : MonoBehaviour
{
    string[] bools = new string[] { "isIdle", "isWalking", "isRunning" };

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToggleState(string state)
    {
        foreach (string b in bools)
            anim.SetBool(b, b == state);
    }
}
