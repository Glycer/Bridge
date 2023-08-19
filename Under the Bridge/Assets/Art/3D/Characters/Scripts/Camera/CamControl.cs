using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class CamControl : MonoBehaviour
{
    public TargetingHUD targetingHUD;

    public bool locked;

    public float tumbleSpeed;
    public float lookSpeed;

    public Transform turn;
    public Transform offset;
    public Camera cam;
    public Transform focusTarget;

    public PlayerMotion playerMotion;
    public CharacterMotion characterMotion;

    public CamLockOn lockOn;
    Coroutine lockOnRotation;
    public GameObject crosshair;
    
    public LookAtConstraint camLook;

    const float LOOK_DELAY = 0.02f;

    const float Y_LIMIT = 65;
    const float MAX_CAM_SPEED = 50;

    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            if ((Input.GetAxis(Inputs.playerHAxis) != 0 || Input.GetAxis(Inputs.playerVAxis) != 0) && !PlayerMotion.MotionLocked())
                characterMotion.RotateCharacter();

            if (Inputs.camHAxis != "0")
                turn.RotateAround(turn.position, Vector3.up, Input.GetAxis(Inputs.camHAxis) * tumbleSpeed * Time.deltaTime);
            if (Inputs.camVAxis != "0")
                turn.RotateAround(turn.position, turn.right, GetCamSpeed());
        }
        else
        {
            if ((Input.GetAxis(Inputs.playerHAxis) != 0 || Input.GetAxis(Inputs.playerVAxis) != 0) && !PlayerMotion.MotionLocked())
                characterMotion.RotateCharacter();
        }
    }
    float GetCamSpeed()
    {
        float CamSlowFactor = GetCamSlowFactor();

        float CamSpeed = -Input.GetAxis(Inputs.camVAxis) * tumbleSpeed * Time.deltaTime * CamSlowFactor;
        // Hard lock on speed to keep the camera tumbling where it should not
        if (CamSpeed < MAX_CAM_SPEED && CamSpeed > -MAX_CAM_SPEED)
            return CamSpeed;
        else if (CamSpeed > MAX_CAM_SPEED)
            return MAX_CAM_SPEED;
        else
            return -MAX_CAM_SPEED;
    }
    float GetCamSlowFactor()
    {
        // Gets a multiple to keep the camera from moving too far upwards or downwards
        if (turn.eulerAngles.x < (360 - Y_LIMIT) && turn.eulerAngles.x > 180 && Input.GetAxis(Inputs.camVAxis) > 0)
            return 1 - ((turn.eulerAngles.x - (360 - Y_LIMIT)) / -10);
        else if (turn.eulerAngles.x > Y_LIMIT && turn.eulerAngles.x < 180 && Input.GetAxis(Inputs.camVAxis) < 0)
            return 1 - ((turn.eulerAngles.x - Y_LIMIT) / 10);
        else
            return 1;
    }

    public void StartLockOnRotation()
    {
        lockOnRotation = StartCoroutine(LockOnAutoRotate());
    }
    public void StopLockOnRotation()
    {
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

            targetingHUD.TrackTarget(cam.WorldToScreenPoint(lockOn.targetLocation.position));

            yield return new WaitForSeconds(LOOK_DELAY);

            // Delock when target gets out of range
            if (!lockOn.lockOffCollider.targets.Contains(lockOn.lockTarget.GetComponent<Collider>()))
                lockOn.ToggleLook(false);
            // Relock when target dies
            else if (!lockOn.lockTarget.gameObject.activeSelf)
                lockOn.ToggleLook(true);
        }
    }
}
