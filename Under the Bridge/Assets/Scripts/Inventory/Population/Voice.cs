using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice
{
    public SentenceStructure structure;

    //Chance of phrasing a statement as a positive ("it is this") versus a negative ("it's not that")
    public float statementPositivity;

    //Chance of phrasing as a statement versus a question
    public float statementAssertion;

    public string desirable;
    public string[] directAddress;

    public Voice()
    {
        structure = new SentenceStructure();

        statementPositivity = Random.Range(0, 1);
        statementAssertion = Random.Range(0, 1);

        desirable = VoiceDatabase.desirable[Random.Range(0, VoiceDatabase.desirable.Length)];
        directAddress = VoiceDatabase.directAddresses[Random.Range(0, VoiceDatabase.directAddresses.Length)];
    }

    public Voice(string[] _directAddress)
    {
        directAddress = _directAddress;
    }
}