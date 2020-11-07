using UnityEngine;

public static class Inputs
{
    public static string playerHAxis = "PlayerHorizontal";
    public static string playerVAxis = "PlayerVertical";
    public static string playerStrafeAxis = "Strafe";

    public static string camHAxis = "CamHorizontal"; /*"Mouse X";*/
    public static string camVAxis = "CamVertical"; /*"Mouse Y";*/

    public static KeyCode centerCam = KeyCode.C;

    public static KeyCode lockOn = KeyCode.Return;
    public static KeyCode lockOff = KeyCode.Backspace;

    public static KeyCode swapIn1 = KeyCode.Z;
    public static KeyCode swapIn2 = KeyCode.X;
    public static KeyCode swapIn3 = KeyCode.C;

    public static KeyCode jump = KeyCode.Space;
    public static KeyCode sprint = KeyCode.LeftShift;

    public static KeyCode whack = KeyCode.Mouse0;
    public static KeyCode secondary = KeyCode.Mouse1;
    public static KeyCode defense = KeyCode.R;

    public static KeyCode ability1 = KeyCode.Alpha1;
    public static KeyCode ability2 = KeyCode.Alpha2;
    public static KeyCode ability3 = KeyCode.Alpha3;
    public static KeyCode ability4 = KeyCode.Alpha4;

    public static KeyCode townView = KeyCode.T;
    public static KeyCode menu = KeyCode.LeftControl;
    public static KeyCode inventory = KeyCode.I;
}
