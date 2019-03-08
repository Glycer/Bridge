using UnityEngine;
using UnityEngine.Animations;

public class CamLockOn : MonoBehaviour
{
    public Transform lockTarget;

    KeyCode lockOn = Inputs.lockOn;
    bool isLockedOn = false;

    CamControl camControl;

    LookAtConstraint look;
    ConstraintSource player;
    ConstraintSource target;

    private void Start()
    {
        camControl = GetComponent<CamControl>();
        look = GetComponent<LookAtConstraint>();

        player = look.GetSource(0);
        target = look.GetSource(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(lockOn) && isLockedOn)
            ToggleLook(false);
        else if (Input.GetKeyDown(lockOn) /*&& lockTarget != null*/)
            ToggleLook(true);
    }

    void ToggleLook(bool _isLocked)
    {
        Transform _targeter = target.sourceTransform;

        isLockedOn = _isLocked;

        camControl.enabled = !_isLocked;

        player.weight = _isLocked ? 0 : 1;
        target.weight = _isLocked ? 1 : 0;

        look.SetSource(0, player);
        look.SetSource(1, target);
        /*
        _targeter.parent = _isLocked ? lockTarget : player.sourceTransform;
        _targeter.localPosition = Vector3.zero;
        _targeter.localRotation = new Quaternion(0, 0, 0, 0);
        */
    }
}
