using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CamLockOn : MonoBehaviour
{
    public CamControl camControl;
    public TargetCollider lockOnCollider;
    public TargetCollider lockOffCollider;

    //The current enemy to lock onto
    public Transform lockTarget;

    public LookAtConstraint focusLook;
    
    bool isLockedOn = false;

    public Transform playerLocation;
    public Transform targetLocation;

    int index = 0;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Inputs.lockOn) && lockOnCollider.targets.Count > 0)
        {
            if (camControl.isAiming)
                camControl.Aim(false);
            ToggleLook(true);
        }

        if (Input.GetKeyDown(Inputs.lockOff))
            ToggleLook(false);

        // Delock when target gets out of range
        if (lockTarget != null && !lockOffCollider.targets.Contains(lockTarget.GetComponent<Collider>()))
            ToggleLook(false);

        // Delock when target dies
        if (lockTarget != null && !lockTarget.gameObject.activeSelf)
            ToggleLook(false);
    }

    public void ToggleLook(bool _isLocked)
    {
        lockOnCollider.RefreshList();
        if (index >= lockOnCollider.targets.Count)
            index = 0;

        isLockedOn = _isLocked;

        if (_isLocked != camControl.locked)
        {
            if (_isLocked)
                camControl.StartLockOnRotation();
            else
                camControl.StopLockOnRotation();
        }
        camControl.locked = _isLocked;

        focusLook.constraintActive = _isLocked;

        if (lockOnCollider.targets.Count == 0)
            return;

        if (_isLocked)
            lockTarget = lockOnCollider.targets[index].transform;
        else
            lockTarget = null;

        targetLocation.parent = _isLocked ? lockTarget : playerLocation;
        targetLocation.localPosition = Vector3.zero;
        targetLocation.localRotation = new Quaternion(0, 0, 0, 0);

        index++;
    }
}
