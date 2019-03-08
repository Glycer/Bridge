using System.Collections;
using UnityEngine;

public class FallCheck : MonoBehaviour {

    Vector3 origin;
    float tooFar = -200;

	// Use this for initialization
	void Start () {
        origin = transform.position;

        StartCoroutine(Check());
	}

    IEnumerator Check()
    {
        while (true)
        {
            if (transform.position.y <= tooFar)
                Hit();
            yield return new WaitForSeconds(1);
        }
    }

    void Hit()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = origin;
    }
}
