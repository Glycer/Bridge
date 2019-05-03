using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersonalityLogic
{
    public enum Attitude { Agreeable, Awed, Sympathetic, Confrontational, Detached, Snide, Whimsical };
    public enum Structure { Direct, Roundabout, Vague, Speculative };

    public static Attitude[] attitudes = new Attitude[] {
        Attitude.Agreeable, Attitude.Awed, Attitude.Confrontational, Attitude.Detached,
        Attitude.Snide, Attitude.Sympathetic, Attitude.Whimsical
    };

    public static Structure[] structures = new Structure[] {
        Structure.Direct, Structure.Roundabout, Structure.Vague
    };

    public static void StructureSpeed(Structure structure)
    {

    }

    public static void OrientSpeech(Attitude attitude)
    {
        //Change how a character talks based on their attitude
    }
}
