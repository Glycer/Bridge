using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersonalityLogic
{
    public enum Attitude { Agreeable, Afraid, Awed, Confrontational, Confused, Detached, Snide, Sympathetic, Whimsical };
    public enum Structure { Direct, Roundabout, Vague, Speculative };

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
