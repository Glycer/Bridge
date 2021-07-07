using System.Collections.Generic;
using UnityEngine;

public class Citizen
{
    public string Name { get; set; }
    public Personality personality;
    public Dictionary<string, Relationship> socialCircle;
    public History history;
    public bool isMale;

    public Citizen()
    {
        Name = "";

        personality = new Personality();
        personality.voice.name = Name;

        socialCircle = new Dictionary<string, Relationship>();
        history = new History();
        isMale = Random.Range(0, 2) == 0;

        PopulationCollection.AddCitizen(this);
    }
}
