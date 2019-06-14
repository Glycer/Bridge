using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Placement : MonoBehaviour {

    public const float DELAY = .1f;
    public const int FIELD_MASK = 1 << 9;
    public const int PLACEABLE_MASK = 1 << 11;
    public const float RANGE = 100;

    public UIManager UI;

    public Buildings buildings;

    public Camera playerCam;
    GameObject placeeGhost;
    GameObject placee;

    GameObject ghost;

    Coroutine placer;
    public bool isPlacing;
    
    public static bool isClearing;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeActivatePlacer();
            DeActivateClearer();
        }
    }

    #region Placing
    public void TogglePlacer(Text objectName)
    {
        if (!isPlacing)
        {
            // Sets the prefabs
            placee = buildings.GetBuilding(objectName.text);
            placeeGhost = buildings.GetGhost(objectName.text);
            // Closes placeables
            UI.DeactivatePlaceables();

            if (isClearing)
                DeActivateClearer();

            ghost = Instantiate(placeeGhost);

            placer = StartCoroutine(Placer());
            isPlacing = true;
        }
        else
            DeActivatePlacer();
    }

    public void DeActivatePlacer()
    {
        if (placer != null)
            StopCoroutine(placer);

        Destroy(ghost);
        isPlacing = false;
    }

    IEnumerator Placer()
    {
        RaycastHit hit;
        Ray ray;

        while (true)
        {
            ray = playerCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, RANGE, FIELD_MASK))
                ghost.transform.position = hit.point;

            yield return new WaitForSeconds(DELAY);
        }
    }

    public void Place()
    {
        Instantiate(placee, ghost.transform.position, ghost.transform.rotation);
    }
    #endregion

    #region Clearing
    public void ToggleClearer()
    {
        if (!isClearing)
        {
            if (placer != null)
                DeActivatePlacer();

            isClearing = true;
        }
        else
            DeActivateClearer();
    }

    void DeActivateClearer()
    {
        isClearing = false;
    }
    #endregion
}