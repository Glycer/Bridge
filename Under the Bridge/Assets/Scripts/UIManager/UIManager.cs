using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryDisplay;
    public StatusBars statusBars;
    public Image[] abilities;
    public GameObject townUI;
    public GameObject placeables;
    public GameObject placeButton;
    public GameObject clearButton;
    public GameObject backButton;
    public Text placeButtonText;
    public Text clearButtonText;
    public Placement placement;

    // 0, Wyatt. 1, Vasilisa. 2, Hanzo.
    public GameObject[] characterHUDs;

    // Wyatt elements
    public GameObject reloadElements;
    public RectTransform reloadBar;
    public Text ammoCountDisplay;

    // Start is called before the first frame update
    void Start()
    {
        inventoryDisplay.SetActive(false);
        townUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(Inputs.menu))
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        if (Input.GetKeyDown(Inputs.inventory))
            inventoryDisplay.SetActive(!inventoryDisplay.activeSelf);
        if (Input.GetKeyDown(Inputs.townView))
            townUI.SetActive(!townUI.activeSelf);
    }

    // Used when health or mana is used or gained
    public void AdjustStatus(int statusIndex, float percentage)
    {
        statusBars.AdjustBar(statusIndex, percentage);
    }
    public void AdjustAbilityDisplay(int abilityIndex, Sprite abilityImage)
    {
        if (abilityImage != null)
        {
            if (!abilities[abilityIndex].gameObject.activeSelf)
                abilities[abilityIndex].gameObject.SetActive(true);
            abilities[abilityIndex].sprite = abilityImage;
        }
        else
            abilities[abilityIndex].gameObject.SetActive(false);
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

    public void SwitchCharacter(int characterIndex)
    {
        for (int i = 0; i < characterHUDs.Length; i++)
        {
            characterHUDs[i].SetActive(i == characterIndex);
        }
    }

    // Wyatt HUD
    public void SetReloadBar(float reloadProgress)
    {
        if (reloadProgress == 0)
            reloadElements.SetActive(false);
        else
        {
            if (!reloadElements.activeSelf)
                reloadElements.SetActive(true);
            reloadBar.sizeDelta = new Vector2(reloadProgress, 1);
        }
    }
    public void SetAmmoDisplay(int ammoCount)
    {
        ammoCountDisplay.text = ammoCount > 0 ? ammoCount.ToString() : "";
    }
}
