using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoSkills : MonoBehaviour
{
    public PlayerMotion motion;
    float baseRunSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        baseRunSpeed = motion.runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Inputs.whack))
        {
            GetComponent<Animator>().Play("SwordSwing");
        }
    }

    private void OnEnable()
    {
        motion.runSpeed *= 2;
    }

    private void OnDisable()
    {
        motion.runSpeed = baseRunSpeed;
    }
}
