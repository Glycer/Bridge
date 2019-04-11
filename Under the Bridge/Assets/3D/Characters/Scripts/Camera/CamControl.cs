using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform cam;

    public float speed;
    public float spinSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis(Inputs.playerHAxis) * speed * Time.deltaTime, 0);
        
        cam.RotateAround(transform.position, Vector3.up, Input.GetAxis(Inputs.camHAxis) * spinSpeed);
    }
}
