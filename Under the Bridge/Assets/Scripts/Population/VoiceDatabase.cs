using System.Collections.Generic;

public static class VoiceDatabase
{
    #region Wyatt, Vasilisa and Hanzo
    public const int TITLE_INDEX = 5;

    public static Dictionary<string, string[]> directAddresses = new Dictionary<string, string[]> {
        { "Wyatt", new string[] {
            "", "Wyatt", "cowboy", "buddy", "pal", "bro", "kid", "kiddo", "boyo", "my liege", "sir", "mister", "Master", "Lord", "Milord"
        } },
        { "Vasilisa", new string[] {
            "", "Vasilisa", "witch", "buddy", "pal", "sis", "kid", "kiddo", "girly", "my liege", "sir", "miss", "Mistress", "Lady", "Milady"
        } },
        { "Hanzo", new string[] {
            "", "Hanzo", "samurai", "buddy", "pal", "bro", "kid", "kiddo", "boyo", "my liege", "sir", "mister", "Master", "Lord", "Milord"
        } }
    };
    #endregion

    #region General Vocabulary
    public static string[] desirable = new string[] {
        "beautiful", "charming", "cool", "crazy", "delightful", "fantastic", "fortunate", "glorious", "gnarly", "great", "insane", "lovely",
        "marvelous", "neat", "neato", "shiny", "stunning", "stupendous", "sweet", "radical", "wonderful"
    };

    public static string[] emphasis = new string[] {
        "awfully", "exceptionally", "literally", "pretty", "quite", "really", "seriously", "super", "very"
    };

    public static string[] filler = new string[] {
        "", "ah", "eh", "ehh", "er", "I mean", "see", "that is", "uh", "um", "well", "you see"
    };

    public static string[] goodbye = new string[] {
        "", "bye", "bye bye", "farewell", "goobye", "see ya", "see you", "smell ya later", "until later"
    };

    public static string[] now = new string[] {
        "at the moment", "right now", "this evening", "tonight"
    };

    public static string[] hello = new string[]
    {
        "", "greetings", "hello", "hey", "hey there", "howdy", "hi", "I see you", "oh, hi", "there you are", "you're here"
    };

    public static string[] partial = new string[]
    {
        "", "a bit", "kinda", "kind of", "rather", "sorta", "sort of"
    };

    public static string[] undesirable = new string[]
    {
        "gross", "nasty", "repulsive", "stinking", "stupid", "terrible", "ugly", "unfortunate", "the worst"
    };

    public static string[][] Vocabulary = { desirable, emphasis, filler, goodbye, hello, now, partial, undesirable };
    #endregion

    #region Opinion
    public static string Dislike()
    {
        string _dislike = "";

        return _dislike;
    }

    public static string Like()
    {
        string _like = "";

        return _like;
    }
    #endregion
}