using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class CamControl : MonoBehaviour
{
    public bool locked;

    public float tumbleSpeed;
    public float lookSpeed;

    public Transform turn;
    public Transform offset;
    public Transform cam;
    public Transform focusTarget;

    public PlayerMotion playerMotion;
    public CharacterMotion characterMotion;
    public GameObject crosshair;
    
    public LookAtConstraint camLook;

    CamZoom camZoom;
    public bool isAiming;

    Vector3 basePos;
    Vector3 aimingPos;
    Vector3 interPos;

    Vector3 focusPlayerPos;
    Vector3 focusAimPos;

    private void Start()
    {
        camZoom = GetComponent<CamZoom>();

        basePos = offset.localPosition;
        aimingPos = new Vector3(.3f, -.1f, -.5f);
        interPos = basePos;

        focusPlayerPos = focusTarget.position;
        focusAimPos = new Vector3(0.3f, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            if (isAiming)
            {
                playerMotion.Turn(Inputs.camHAxis, 2);
                turn.RotateAround(turn.position, turn.right, GetCamSpeed());
                //characterMotion.LookVertical(Inputs.camVAxis);
                //focusTarget.Translate(0, Input.GetAxis(Inputs.camVAxis) * lookSpeed * Time.deltaTime, 0);
            }
            else
            {
                if (Input.GetAxis(Inputs.playerHAxis) != 0 || Input.GetAxis(Inputs.playerVAxis) != 0)
                    characterMotion.RotateCharacter();

                turn.RotateAround(turn.position, Vector3.up, Input.GetAxis(Inputs.camHAxis) * tumbleSpeed * Time.deltaTime);
                turn.RotateAround(turn.position, turn.right, GetCamSpeed());
            }
        }
    }
    float GetCamSpeed()
    {
        // Gets a multiple to keep the camera from moving too far upwards or downwards
        float CamSlowFactor;

        if (turn.eulerAngles.x < 310 && turn.eulerAngles.x > 180 && -Input.GetAxis(Inputs.camVAxis) < 0)
            CamSlowFactor = 1 - ((turn.eulerAngles.x - 310) / -10);
        else if (turn.eulerAngles.x > 50 && turn.eulerAngles.x < 180 && -Input.GetAxis(Inputs.camVAxis) > 0)
            CamSlowFactor = 1 - ((turn.eulerAngles.x - 50) / 10);
        else
            CamSlowFactor = 1;

        float CamSpeed = -Input.GetAxis(Inputs.camVAxis) * tumbleSpeed * Time.deltaTime * CamSlowFactor;
        // Hard lock on speed to keep the camera tumbling where it should not
        if (CamSpeed < 50 && CamSpeed > -50)
            return CamSpeed;
        else if (CamSpeed > 50)
            return 50;
        else
            return -50;
    }

    public void Aim(bool _isAiming)
    {
        if (!locked)
        {
            isAiming = _isAiming;

            crosshair.SetActive(_isAiming);
            camZoom.ToggleLook(_isAiming);

            // Turns to face camera
            characterMotion.ResetRotation();

            if (_isAiming)
            {
                playerMotion.SetDirection();
                camZoom.ZoomTo(aimingPos, focusAimPos);
            }
            else
                camZoom.ZoomTo(interPos, focusPlayerPos);
        }
    }

    public void RotatePlayer(Quaternion newRotation)
    {
        turn.transform.rotation *= newRotation;
    }
}
