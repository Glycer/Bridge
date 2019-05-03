using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoiceDatabase
{
    //Most direct addresses are accessed by directAddress[0]
    //  If it's empty, get the addressee's name
    //  A gendered direct address is directAddress[0] for male, directAddress[1] for female.
    public static string[][] directAddresses = new string[][] {
        new string[] { "" },
        new string[] { "buddy" },
        new string[] { "pal" },
        new string[] { "bro" },
        new string[] { "mister", "miss" },
        new string[] { "sir", "milady" },
        new string[] { "boyo", "girly" }
    };

    public static string[] desirable = new string[] { "beautiful", "charming", "cool", "crazy", "delightful",
        "gnarly", "insane", "lovely", "neat", "neato", "shiny", "sweet"
    };
}
