using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryDisplay;

    // Start is called before the first frame update
    void Start()
    {
        inventoryDisplay.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(Inputs.inventory))
            inventoryDisplay.SetActive(!inventoryDisplay.activeSelf);
    }
}
