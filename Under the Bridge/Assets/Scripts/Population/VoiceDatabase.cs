using System.Collections.Generic;

public static class VoiceDatabase
{
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

    public static string[] desirable = new string[] {
        "beautiful", "charming", "cool", "crazy", "delightful", "fantastic", "glorious", "gnarly",
        "insane", "lovely", "marvelous", "neat", "neato", "shiny", "sweet", "wondrous"
    };

    public static string[] now = new string[] {
        "at the moment", "right now", "this evening", "this morning", "today", "tonight"
    };

    public static string[] salutation = new string[]
    {
        "", "greetings", "hello", "hey", "hey there", "hi", "I see you", "oh, hi", "there you are", "you're here"
    };

    public static string[] undesirable = new string[]
    {
        "gross", "repulsive", "terrible", "ugly"
    };

    public static string[][] Vocabulary = { desirable, now, salutation, undesirable };
}