using System.Collections;
using UnityEngine;

public class Placement : MonoBehaviour {

    public const float DELAY = .1f;
    public const int FIELD_MASK = 1 << 9;
    public const int PLACEABLE_MASK = 1 << 11;
    public const float RANGE = 100;

    public UIManager UI;

    public Camera playerCam;
    public GameObject placeeGhost;
    public GameObject placee;

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
    public void TogglePlacer()
    {
        if (!isPlacing)
        {
            if (isClearing)
                DeActivateClearer();

            ghost = Instantiate(placeeGhost);

            placer = StartCoroutine(Placer());
            isPlacing = true;
        }
        else
            DeActivatePlacer();
    }

    void DeActivatePlacer()
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

    /*public void SetPlacee(GameObject toBePlacee)
    {
        placee = toBePlacee;
        placeeGhost = TownObjects.GetGhost(toBePlacee);
    }*/

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
            UI.ToggleClear();

            if (placer != null)
                DeActivatePlacer();

            isClearing = true;
        }
        else
            DeActivateClearer();
    }

    void DeActivateClearer()
    {
        UI.ToggleClear();
        isClearing = false;
    }
    #endregion
}