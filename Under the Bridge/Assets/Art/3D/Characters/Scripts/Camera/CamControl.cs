using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class CamControl : MonoBehaviour
{
    public UIManager UI;

    public bool locked;

    public float tumbleSpeed;
    public float lookSpeed;

    public Transform turn;
    public Transform offset;
    public Transform cam;
    public Transform focusTarget;

    public PlayerMotion playerMotion;
    public CharacterMotion characterMotion;
    // Collider follows camera and must be adjusted during aim,
    // will probably want to be replaced with a better solution in the future
    CapsuleCollider currWyattCollider;

    public CamLockOn lockOn;
    Coroutine lockOnRotation;
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

        currWyattCollider = null;
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
                if ((Input.GetAxis(Inputs.playerHAxis) != 0 || Input.GetAxis(Inputs.playerVAxis) != 0) && !characterMotion.rotationLocked)
                    characterMotion.RotateCharacter();

                turn.RotateAround(turn.position, Vector3.up, Input.GetAxis(Inputs.camHAxis) * tumbleSpeed * Time.deltaTime);
                turn.RotateAround(turn.position, turn.right, GetCamSpeed());
            }
        }
        else
        {
            if ((Input.GetAxis(Inputs.playerHAxis) != 0 || Input.GetAxis(Inputs.playerVAxis) != 0) && !characterMotion.rotationLocked)
                characterMotion.RotateCharacter();
        }
    }
    float GetCamSpeed()
    {
        float CamSlowFactor = GetCamSlowFactor();

        float CamSpeed = -Input.GetAxis(Inputs.camVAxis) * tumbleSpeed * Time.deltaTime * CamSlowFactor;
        // Hard lock on speed to keep the camera tumbling where it should not
        if (CamSpeed < 50 && CamSpeed > -50)
            return CamSpeed;
        else if (CamSpeed > 50)
            return 50;
        else
            return -50;
    }
    float GetCamSlowFactor()
    {
        // Gets a multiple to keep the camera from moving too far upwards or downwards
        if (turn.eulerAngles.x < 310 && turn.eulerAngles.x > 180 && Input.GetAxis(Inputs.camVAxis) > 0)
            return 1 - ((turn.eulerAngles.x - 310) / -10);
        else if (turn.eulerAngles.x > 50 && turn.eulerAngles.x < 180 && Input.GetAxis(Inputs.camVAxis) < 0)
            return 1 - ((turn.eulerAngles.x - 50) / 10);
        else
            return 1;
    }

    public void Aim(bool _isAiming, CapsuleCollider collider = null)
    {
        if (collider != null)
            currWyattCollider = collider;
        if (locked)
            lockOn.ToggleLook(false);

        isAiming = _isAiming;

        crosshair.SetActive(_isAiming);
        camZoom.ToggleLook(_isAiming);

        // Turns to face camera
        characterMotion.ResetRotation();

        if (_isAiming)
        {
            playerMotion.SetDirection();
            camZoom.ZoomTo(aimingPos, focusAimPos);
            currWyattCollider.center = new Vector3(currWyattCollider.center.x, -1.5f, currWyattCollider.center.z);
        }
        else
        {
            camZoom.ZoomTo(interPos, focusPlayerPos);
            currWyattCollider.center = new Vector3(currWyattCollider.center.x, -2.5f, currWyattCollider.center.z);
        }
    }

    public void StartLockOnRotation()
    {
        UI.ActivateReticule(true);
        lockOnRotation = StartCoroutine(LockOnAutoRotate());
    }
    public void StopLockOnRotation()
    {
        UI.ActivateReticule(false);
        if (lockOnRotation != null)
            StopCoroutine(lockOnRotation);
    }
    IEnumerator LockOnAutoRotate()
    {
        float xDistance;
        float yDistance;
        while (true)
        {
            yDistance = focusTarget.eulerAngles.y - turn.eulerAngles.y;
            if (yDistance > 180)
                yDistance -= 360;
            else if (yDistance < -180)
                yDistance += 360;
            xDistance = focusTarget.eulerAngles.x - turn.eulerAngles.x;
            if (xDistance > 180)
                xDistance -= 360;
            else if (xDistance < -180)
                xDistance += 360;

            if (xDistance > 70 || xDistance < -70)
                xDistance *= 2;
            if (yDistance > 70 || yDistance < -70)
                yDistance *= 2;

            turn.RotateAround(turn.position, Vector3.up, yDistance / 30);
            turn.RotateAround(turn.position, turn.right, GetCamSlowFactor() * xDistance / 30);

            UI.TrackTarget(new Vector3(yDistance * 8, xDistance * 8 + 30, 0));

            yield return new WaitForSeconds(0.02f);
        }
    }
}
