using UnityEngine;

public class BuildField : MonoBehaviour {

    public Placement placement;

    private void OnMouseDown()
    {
        if (placement.isPlacing)
            placement.Place();
    }
}
