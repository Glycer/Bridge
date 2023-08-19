using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CamLockOn : MonoBehaviour
{
    public CamControl camControl;
    public TargetCollider lockOnCollider;
    public TargetCollider lockOffCollider;

    public TargetingHUD targetingHUD;

    //The current enemy to lock onto
    public Transform lockTarget;

    public LookAtConstraint focusLook;
    
    bool isLockedOn = false;

    public Transform playerLocation;
    public Transform targetLocation;

    public GameObject gunColliderRoot;

    int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Inputs.lockOn) && lockOnCollider.targets.Count > 0)
            ToggleLook(true);
        else if (Input.GetKeyDown(Inputs.lockOff))
            ToggleLook(false);
    }

    public void ToggleLook(bool _isLocked)
    {
        lockOnCollider.RefreshList();
        lockOffCollider.RefreshList();
        if (lockOnCollider.targets.Count != 0)
        {
            if (index >= lockOnCollider.targets.Count)
                index = 0;

            isLockedOn = _isLocked;

            if (_isLocked)
            {
                lockTarget = lockOnCollider.targets[index].transform;
                targetingHUD.ActivateTargeting(true, lockTarget);
                gunColliderRoot.GetComponent<LookAtConstraint>().constraintActive = true;
            }
            else
            {
                lockTarget = null;
                targetingHUD.ActivateTargeting(false, lockTarget);
                gunColliderRoot.GetComponent<LookAtConstraint>().constraintActive = false;
                gunColliderRoot.transform.localRotation = Quaternion.identity;
            }

            if (_isLocked != camControl.locked)
            {
                if (_isLocked)
                    camControl.StartLockOnRotation();
                else
                    camControl.StopLockOnRotation();
            }
            camControl.locked = _isLocked;

            focusLook.constraintActive = _isLocked;

            targetLocation.parent = _isLocked ? lockTarget : playerLocation;
            targetLocation.localPosition = Vector3.zero;
            targetLocation.localRotation = new Quaternion(0, 0, 0, 0);

            index++;
        }
        // Delock if no targets remain
        else
        {
            isLockedOn = false;
            camControl.locked = false;
            camControl.StopLockOnRotation();
            lockTarget = null;
            targetingHUD.ActivateTargeting(false, lockTarget);
            gunColliderRoot.GetComponent<LookAtConstraint>().constraintActive = false;
            gunColliderRoot.transform.localRotation = Quaternion.identity;
            focusLook.constraintActive = false;
            targetLocation.parent = playerLocation;
            targetLocation.localPosition = Vector3.zero;
            targetLocation.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
    public bool IsLockedOn()
    {
        return isLockedOn;
    }
    public Transform GetTarget()
    {
        return lockTarget;
    }
}
