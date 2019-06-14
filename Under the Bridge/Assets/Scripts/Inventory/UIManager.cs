using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryDisplay;
    public GameObject townUI;
    public GameObject placeables;
    public GameObject placeButton;
    public GameObject clearButton;
    public GameObject backButton;
    public Text placeButtonText;
    public Text clearButtonText;
    public Placement placement;

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
        placeButtonText.text = (!placeables.activeSelf ? "Place" : "Back");
        clearButton.SetActive(!clearButton.activeSelf);

    }

    public void DeactivatePlaceables()
    {
        placeables.SetActive(false);
        backButton.SetActive(true);
    }

    public void ActivatePlaceables()
    {
        placeables.SetActive(true);
        backButton.SetActive(false);
        placement.DeActivatePlacer();
    }

    public void ToggleClear()
    {
        placement.ToggleClearer();
        placeButton.SetActive(!placeButton.activeSelf);
        clearButtonText.text = (clearButtonText.text == "Clear" ? "Back" : "Clear");
        
    }
}
