using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice
{
    public SentenceStructure structure;

    //Chance of phrasing a statement as a positive ("it is this") versus a negative ("it's not that")
    public float positivity;

    //Chance of phrasing as a statement versus a question
    public float assertion;

    //0-25% verbosity gives a chance of saying nothing at all.
    //75-100% verbosity gives a chance of repeating an idea in different words.
    public float verbosity;

    bool fullAddress;
    public int directAddress;
    public string address;

    public string desirable;
    public string now;
    public string salutation;
    public string undesirable;

    public Voice()
    {
        structure = new SentenceStructure();

        positivity = Random.Range(0f, 1f);
        assertion = Random.Range(0f, 1f);
        verbosity = Random.Range(0f, 1f);

        directAddress = Random.Range(0, VoiceDatabase.directAddresses["Wyatt"].Length);
        fullAddress = Random.Range(0, 2) == 0 ? true : false;
        SetAddress();

        InitVocab();
    }

    void InitVocab()
    {
        string[] vocabulary = new string[VoiceDatabase.Vocabulary.Length];
        for (int i = 0; i < vocabulary.Length; i++)
            vocabulary[i] = VoiceDatabase.Vocabulary[i][Random.Range(0, VoiceDatabase.Vocabulary[i].Length)];

        desirable = vocabulary[0];
        now = vocabulary[1];
        salutation = vocabulary[2];
        undesirable = vocabulary[3];
    }

    public void SetAddress()
    {
        int addresses = VoiceDatabase.directAddresses["Wyatt"].Length;

        //If direct address is a title, add the PC's name if the speaker would do so.
        address = directAddress > addresses - VoiceDatabase.TITLE_INDEX ? (
            fullAddress ? VoiceDatabase.directAddresses[SwapCharacter.activeChar.name][directAddress]
                + " " + VoiceDatabase.directAddresses[SwapCharacter.activeChar.name][1] :
                VoiceDatabase.directAddresses[SwapCharacter.activeChar.name][directAddress]
            ) :
            VoiceDatabase.directAddresses[SwapCharacter.activeChar.name][directAddress];
    }
}