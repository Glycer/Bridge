using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public int loot;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: figure out how to access attatched texts
        /*loot = 0;
        text = GetComponent<Text>();
        updateDisplay(loot);*/
        gameObject.SetActive(false);
    }

    /*void Update ()
    {
        if (Input.GetKeyDown(Inputs.inventory))
            gameObject.SetActive(true);
        if (Input.GetKeyUp(Inputs.inventory))
            gameObject.SetActive(false);
    }*/

    void updateDisplay(int newValue)
    {
        text.text = newValue.ToString();
    }
}
