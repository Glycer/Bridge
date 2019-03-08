using UnityEngine;

public class Placeable : MonoBehaviour {

    private void OnMouseDown()
    {
        if (Placement.isClearing)
            Destroy(gameObject);
    }
}
