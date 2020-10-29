using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBars : MonoBehaviour
{
    public RectTransform[] bars;

    public void AdjustBar(int statusIndex, float percentage)
    {
        bars[statusIndex].sizeDelta = new Vector2(percentage, 1);
    }
}
