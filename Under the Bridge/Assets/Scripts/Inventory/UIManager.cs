using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryDisplay;
    public GameObject townUI;
    public GameObject placeables;
    public Text placeButton;
    public Text clearButton;

    // Start is called before the first frame update
    void Start()
    {
        inventoryDisplay.SetActive(false);
        townUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(Inputs.inventory))
            inventoryDisplay.SetActive(!inventoryDisplay.activeSelf);
        if (Input.GetKeyDown(Inputs.townView))
            townUI.SetActive(!townUI.activeSelf);
    }

    public void TogglePlaceables()
    {
        placeables.SetActive(!placeables.activeSelf);
        placeButton.text = (!placeables.activeSelf ? "Place" : "Back");
    }

    public void ToggleClear()
    {
        clearButton.text = (clearButton.text == "Clear" ? "Back" : "Clear");
    }
}
