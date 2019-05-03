using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attitude
{
    //Each character has a default attitude, but his/her attitude may change for certain subjects
    public PersonalityLogic.Attitude defaultAttitude;
    public PersonalityLogic.Attitude currentAttitude;

    public Attitude()
    {
        defaultAttitude = PersonalityLogic.attitudes[Random.Range(0, PersonalityLogic.attitudes.Length)];
    }

    public Attitude(PersonalityLogic.Attitude attitude)
    {
        defaultAttitude = attitude;
    }
}
