using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(Inputs.menu))
        {
            Cursor.lockState =
            Cursor.lockState == CursorLockMode.Locked ?
            CursorLockMode.None :
            CursorLockMode.Locked;
        }
    }
}
