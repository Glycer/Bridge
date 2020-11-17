using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CamLockOn : MonoBehaviour
{
    public CamControl camControl;
    public TargetCollider targetCollider;

    //The current enemy to lock onto
    public Transform lockTarget;

    public LookAtConstraint playerLook;
    
    bool isLockedOn = false;

    LookAtConstraint look;
    ConstraintSource player;
    ConstraintSource target;

    // for postioning the camera for lock on
    Coroutine orient;

    int index = 0;

    private void Start()
    {
        look = GetComponent<LookAtConstraint>();

        player = look.GetSource(0);
        target = look.GetSource(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Inputs.lockOn) && targetCollider.targets.Count > 0)
            ToggleLook(true);

        if (Input.GetKeyDown(Inputs.lockOff))
            ToggleLook(false);

        // Delock when target gets out of range
        if (lockTarget != null && !targetCollider.targets.Contains(lockTarget.GetComponent<Collider>()))
            ToggleLook(false);

        // Delock when target dies
        if (lockTarget != null && !lockTarget.gameObject.activeSelf)
            ToggleLook(false);
    }

    void ToggleLook(bool _isLocked)
    {
        if (index >= targetCollider.targets.Count)
            index = 0;

        if (isLockedOn != _isLocked && _isLocked == true)
        {
            float duration = .1f;

            if (orient != null)
                StopCoroutine(orient);

            orient = StartCoroutine(Interpolater.InterpolateLocalRotation(camControl.turn, Quaternion.Euler(10, 0, 0), duration));
        }

        //The camera aim target object
        Transform _targeter = target.sourceTransform;

        isLockedOn = _isLocked;
        camControl.pitchIsLocked = _isLocked;

        camControl.enabled = !_isLocked;

        player.weight = _isLocked ? 0 : 1;
        target.weight = _isLocked ? 1 : 0;

        playerLook.constraintActive = _isLocked;

        look.SetSource(0, player);
        look.SetSource(1, target);

        if (targetCollider.targets.Count == 0)
            return;

        if (_isLocked)
            lockTarget = targetCollider.targets[index].transform;
        else
            lockTarget = null;

        _targeter.parent = _isLocked ? lockTarget : player.sourceTransform;
        _targeter.localPosition = Vector3.zero;
        _targeter.localRotation = new Quaternion(0, 0, 0, 0);

        index++;
    }
}
