using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice
{
    public SentenceStructure structure;

    //Assertion: Chance of phrasing as a statement versus a question.
    public float assertion { get; set; }

    //Contraction: Chance of contracting. <20% = never contracts. >40% = always contracts EXCEPT when lying. >80% = always contracts EVEN when lying.
    public float contraction;

    //Directness: Chance of phrasing as direct ("I like him") vs. distanced ("He's nice"). >10% = ALWAYS distances when lying.
    public float distance;

    //Positivity: Chance of phrasing a statement as a positive ("it is this") versus a negative ("it's not that")
    public float positivity;

    //Verbosity: Verbosity. 0-25% chance of saying nothing at all, 75-100% of repeating an idea in different words.
    public float verbosity;

    //Address: How this voice addresses the PC
    bool fullAddress;
    public int addressIndex;
    public string address;

    //Title: Each character has a title by which to address the PC, but most will rarely use it.
    public bool usesTitle;
    public string title;

    public bool iAmMe;
    public string name;

    //Vocabulary variables contain: Which word this voice uses to express this idea
    public string desirable, emphasis, filler, goodbye, hello, now, partial, undesirable;

    //List<string> vocab;

    public Voice()
    {
        structure = new SentenceStructure();

        /* Doesn't work. The intent is to REPLACE THE VARIABLE'S CONTENT, but instead the FLOAT IN THE ARRAY GETS REPLACED
        float[] factors = new float[] { assertion, contraction, distance, positivity, verbosity };
        for (int i = 0; i < factors.Length; i++)
            factors[i] = Random.Range(0f, 1f);
        */

        assertion = Random.Range(0f, 1f);
        contraction = Random.Range(0f, 1f);
        distance = Random.Range(0f, 1f);
        positivity = Random.Range(0f, 1f);
        verbosity = Random.Range(0f, 1f);

        addressIndex = Random.Range(0, VoiceDatabase.directAddresses["Wyatt"].Length);
        fullAddress = Random.Range(0, 2) == 0;
        SetAddress();

        InitVocab();
    }

    void InitVocab()
    {
        /* Doesn't work, same as in Voice()
        string[] vocabulary = new string[VoiceDatabase.Vocabulary.Length];
        for (int i = 0; i < vocabulary.Length; i++)
            vocabulary[i] = VoiceDatabase.Vocabulary[i][Random.Range(0, VoiceDatabase.Vocabulary[i].Length)];
        
        vocab = new List<string>() { desirable, emphasis, filler, goodbye, hello, now, partial, undesirable };

        for (int i = 0; i < vocab.Count; i++)
            vocab[i] = vocabulary[i];
        */

        desirable = VoiceDatabase.desirable[Random.Range(0, VoiceDatabase.desirable.Length)];
        emphasis = VoiceDatabase.emphasis[Random.Range(0, VoiceDatabase.emphasis.Length)];
        filler = VoiceDatabase.filler[Random.Range(0, VoiceDatabase.filler.Length)];
        goodbye = VoiceDatabase.goodbye[Random.Range(0, VoiceDatabase.goodbye.Length)];
        hello = VoiceDatabase.hello[Random.Range(0, VoiceDatabase.hello.Length)];
        now = VoiceDatabase.now[Random.Range(0, VoiceDatabase.now.Length)];
        partial = VoiceDatabase.partial[Random.Range(0, VoiceDatabase.partial.Length)];
        undesirable = VoiceDatabase.undesirable[Random.Range(0, VoiceDatabase.undesirable.Length)];
        
        //There is a 1% chance that a character will refer to hi/rself in the third person.
        iAmMe = Random.Range(0, 1) > .01f;
    }

    public void SetAddress()
    {
        int addresses = VoiceDatabase.directAddresses["Wyatt"].Length;

        //If address index is a title, set the title to it. Otherwise, set title.
        if (addressIndex > addresses - VoiceDatabase.TITLE_INDEX)
        {
            usesTitle = true;
            title = VoiceDatabase.directAddresses[SwapCharacter.activeChar.name][addressIndex];

            //Add PC's name to full address
            address = fullAddress ? title + " " + VoiceDatabase.directAddresses[SwapCharacter.activeChar.name][1] : title;
        }
        else
        {
            title = VoiceDatabase.directAddresses[SwapCharacter.activeChar.name][Random.Range(addresses - VoiceDatabase.TITLE_INDEX, addresses)];
            address = VoiceDatabase.directAddresses[SwapCharacter.activeChar.name][addressIndex];
        }
    }
}