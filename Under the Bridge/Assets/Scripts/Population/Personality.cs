using UnityEngine;

public class Personality
{
    public Voice voice;
    //public Attitude attitude;

    //Energy
    public float baselineEnergy;
    public bool energized;

    //How much does s/he care about justifying hi/r feelings, and how much about backing opinions with evidence?
    public float justificiary;
    public float evidentiary;

    public Personality ()
    {
        voice = new Voice();
        //attitude = new Attitude();

        baselineEnergy = Random.Range(0, 1);
        energized = baselineEnergy > .6f;

        justificiary = Random.Range(0, 1);
        evidentiary = Random.Range(0, 1);
    }
}
