using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float speed;

    string vertical = Inputs.camVAxis;
    string horizontal = Inputs.camHAxis;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis(horizontal) * speed * Time.deltaTime,
            Input.GetAxis(vertical) * speed * Time.deltaTime,
            0);
    }
}
