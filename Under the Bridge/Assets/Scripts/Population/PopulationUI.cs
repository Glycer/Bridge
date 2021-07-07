using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationUI : MonoBehaviour
{
    public ScrollRect scroll;
    public GameObject btnTemplate;
    public PopulationCollection population;

    Dictionary<Citizen, GameObject> citButtons = new Dictionary<Citizen, GameObject>();
    List<GameObject> buttons = new List<GameObject>();

    const int BTN_HEIGHT = 40;

    private void Start()
    {
        Subscribe();
    }

    public void OpenClose(GameObject view)
    {
        view.SetActive(!view.activeSelf);
    }

    void AddButton(Citizen citizen)
    {
        Vector2 offset = new Vector2(0, population.citizens.Count * -BTN_HEIGHT);
        GameObject button = Instantiate(btnTemplate, scroll.content);

        citButtons.Add(citizen, button);
        buttons.Add(button);

        RectTransform rect = button.GetComponent<RectTransform>();
        rect.anchoredPosition = offset;
        UpdateContentSize();

        button.transform.GetChild(0).GetComponent<Text>().text = citizen.Name == "" ? "Unnamed" : citizen.Name;
        button.SetActive(true);
    }

    void RemoveButton(Citizen citizen)
    {
        GameObject button = citButtons[citizen];
        buttons.Remove(button);
        Destroy(button);
        citButtons.Remove(citizen);

        for (int i = 0; i < buttons.Count; i++)
            buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (i + 1) * -BTN_HEIGHT);

        UpdateContentSize();
    }

    void UpdateContentSize()
    {
        Vector2 offset = new Vector2(0, population.citizens.Count * BTN_HEIGHT);
        scroll.content.GetComponent<RectTransform>().sizeDelta = offset;
    }

    void Subscribe()
    {
        PopulationCollection.AddCitizen += AddButton;
        PopulationCollection.DeleteCitizen += RemoveButton;
    }

    void Unsubscribe()
    {
        PopulationCollection.AddCitizen -= AddButton;
        PopulationCollection.DeleteCitizen -= RemoveButton;
    }
}
