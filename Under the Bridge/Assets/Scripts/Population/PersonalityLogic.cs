using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersonalityLogic
{
    public enum Attitude { Agreeable, Awed, Confrontational, Confused, Detached, Fearful, Snide, Sympathetic, Whimsical };
    public enum Structure { Direct, Roundabout, Vague, Speculative };

    public static readonly string[] relationships = new string[] {
        "acquaintance", "boss", "child", "friend", "idol", "parental", "rival", "sibling", "student", "teacher"
    };

    public static Attitude[] attitudes = new Attitude[] {
        Attitude.Agreeable, Attitude.Confrontational
    };

    public static Structure[] structures = new Structure[] {
        Structure.Direct, Structure.Roundabout
    };

    public static void StructureSpeed(Structure structure)
    {

    }

    public static void OrientSpeech(Attitude attitude)
    {
        //Change how a character talks based on their attitude
    }
}
