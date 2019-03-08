using UnityEngine;

public class DomainMap : MonoBehaviour {

    public GameObject map;

	// Use this for initialization
	void Start () {
        map.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
            map.SetActive(map.activeSelf ? false : true);
	}
}
