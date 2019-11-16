using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueDatabase
{
    static Voice voice;

    static void RefreshVoice(Citizen citizen)
    {
        voice = citizen.personality.voice;
    }

    public static string LovelyWeather(Citizen citizen)
    {
        RefreshVoice(citizen);

        //Hello, PC! Lovely weather, isn't it?
        string lovelyWeather = string.Format(
            (voice.salutation == "" ?
                (voice.address == "" ? "{3}" : Linguistics.Capitalize(voice.address) + "{2} {3}") :
                "{0}" + (voice.address == "" ? "{2}" : ", {1}{2}") + " {3}"),
            Linguistics.Capitalize(voice.salutation),
            voice.address,
            (Linguistics.Assert(voice.assertion) ? "!" : "?"),
            (Linguistics.Assert(voice.positivity) ?
                (Linguistics.Assert(voice.assertion) ?
                    (string.Format("The weather is {0} {1}!", voice.desirable, voice.now)) : (Linguistics.Capitalize(voice.desirable) + " weather, isn't it?")
                ) :
                (Linguistics.Assert(voice.assertion) ?
                    (string.Format("The weather isn't {0} {1}.", voice.undesirable, voice.now)) : ("Not " + voice.undesirable + " weather, is it?")
                )
            )
        );

        return lovelyWeather;
    }
}
