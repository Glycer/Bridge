using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relationship
{
    public Citizen subject { get; }
    public string relation { get; set; }
    public string opinion;

    public Relationship(Citizen target, string relationship)
    {
        subject = target;
        relation = relationship;
    }
}
