using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personality
{
    public Voice voice;
    public Attitude attitude;

    public Personality ()
    {
        voice = new Voice();
        attitude = new Attitude();
    }
}
