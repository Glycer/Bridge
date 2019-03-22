using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasilisaSkills : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(Inputs.magic1))
        {
            GetComponent<Animator>().SetBool("isPushing", true);
        }
        if(Input.GetKeyUp(Inputs.magic1))
        {
            GetComponent<Animator>().SetBool("isPushing", false);
        }
    }
}
