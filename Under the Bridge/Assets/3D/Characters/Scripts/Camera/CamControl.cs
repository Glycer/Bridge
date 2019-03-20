using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float speed;

    string horizontal = Inputs.camHAxis;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, -Input.GetAxis(horizontal) * speed * Time.deltaTime, 0);
    }
}
