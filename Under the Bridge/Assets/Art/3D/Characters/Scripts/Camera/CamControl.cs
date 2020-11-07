using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class CamControl : MonoBehaviour
{
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

    public bool pitchIsLocked = false;
    bool lookUpLocked = false;
    bool lookDownLocked = false;

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

        StartCoroutine(PitchLock());
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            playerMotion.Turn(Inputs.camHAxis);
            if (!lookDownLocked && !lookUpLocked)
                turn.RotateAround(turn.position, turn.right, Input.GetAxis(Inputs.camVAxis) * tumbleSpeed * Time.deltaTime);
            //characterMotion.LookVertical(Inputs.camVAxis);
            //focusTarget.Translate(0, Input.GetAxis(Inputs.camVAxis) * lookSpeed * Time.deltaTime, 0);
        }
        else
        {
            turn.RotateAround(turn.position, Vector3.up, -Input.GetAxis(Inputs.camHAxis) * tumbleSpeed * Time.deltaTime);
            if (!lookDownLocked && !lookUpLocked)
                turn.RotateAround(turn.position, turn.right, Input.GetAxis(Inputs.camVAxis) * tumbleSpeed * Time.deltaTime);
        }
    }

    public void Aim(bool _isAiming)
    {
        isAiming = _isAiming;

        crosshair.SetActive(_isAiming);
        camZoom.ToggleLook(_isAiming);

        if (_isAiming)
            camZoom.ZoomTo(aimingPos, focusAimPos);
        else
            camZoom.ZoomTo(interPos, focusPlayerPos);
    }

    IEnumerator PitchLock()
    {
        string vertical = Inputs.camVAxis;

        while (true)
        {
            if (cam.rotation.eulerAngles.x > 75 && cam.rotation.eulerAngles.x < 90)
                lookUpLocked = true;
            if (cam.rotation.eulerAngles.x > 270 && cam.rotation.eulerAngles.x < 285)
                lookDownLocked = true;

            if (!pitchIsLocked)
            {
                if (lookUpLocked && Input.GetAxis(vertical) < 0)
                    lookUpLocked = false;
                else if (lookDownLocked && Input.GetAxis(vertical) > 0)
                    lookDownLocked = false;
            }

            yield return new WaitForSeconds(.05f);
        }
    }
}
