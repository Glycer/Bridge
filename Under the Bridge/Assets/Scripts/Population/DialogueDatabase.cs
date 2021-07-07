public static class DialogueDatabase
{
    static Voice voice;

    static void RefreshVoice(Citizen citizen)
    {
        voice = citizen.personality.voice;
    }

    #region Niceties
    public static string FormatHelloGoodbye(string word, string address, bool emptyAddress, bool energized)
    {
        string helloGoodbye = string.Format(
            word == "" ?
                (emptyAddress ? "..." : Linguistics.Capitalize(voice.address) + "{2}") :
                "{0}" + (emptyAddress ? "{2}" : ", {1}{2}"),
            Linguistics.Capitalize(word),                                                                       //0
            address,                                                                                            //1
            Linguistics.Energize(energized, Linguistics.Assert(voice.assertion + .6f /*Decreased odds of ?*/))  //2
        );

        return helloGoodbye;
    }

    public static string HelloGoodbye(Citizen citizen, bool isHello)
    {
        RefreshVoice(citizen);

        string word;
        string address;
        bool emptyAddress;

        if (isHello)
        {
            word = voice.hello;
            address = voice.address;
            emptyAddress = address == "";
        }
        else
        {
            word = voice.goodbye;
            address = voice.title;
            emptyAddress = voice.usesTitle;
        }

        return FormatHelloGoodbye(word, address, emptyAddress, citizen.personality.energized);
    }

    public static string LovelyWeather(Citizen citizen)
    {
        RefreshVoice(citizen);

        bool positive = Linguistics.Assert(voice.positivity);
        bool assert = Linguistics.Assert(voice.assertion);
        bool addNow = voice.verbosity > .4f;

        string weatherAdjective = positive ? voice.desirable : voice.undesirable;
        string now = addNow ? " " + voice.now : "";

        //Lovely weather, isn't it?
        string lovelyWeather = string.Format(
            "{0}{1}",
            assert ?                                                    //0
                    string.Format("The weather{2} {0}{1}",
                        weatherAdjective,                   //0
                        now,                                //1
                        positive ?                          //2
                            Linguistics.Contract(Linguistics.IS, voice.contraction, false) :
                            Linguistics.Contract(Linguistics.IS_NOT, voice.contraction, false)
                    ) :
                    (positive ?
                        string.Format("{0} weather{1},{2}",
                            Linguistics.Capitalize(weatherAdjective),                               //0
                            now,                                                                    //1
                            Linguistics.Contract(Linguistics.IS_IT_NOT, voice.contraction, false)   //2
                        ) :
                        string.Format("Not {0} weather{1}, is it", weatherAdjective, now)
                    ),
            Linguistics.Energize(citizen.personality.energized, assert) //1
        );

        return lovelyWeather;
    }
    #endregion

    #region Shopping
    public static string CanIInterestYou(Citizen citizen)
    {
        RefreshVoice(citizen);

        //What can I interest you in tonight?
        string shop = string.Format("What can {0} interest you in {1}{2}",
            Linguistics.Me(voice, true),                                    //0
            voice.now,                                                      //1
            Linguistics.Energize(citizen.personality.energized, false)      //2
        );

        return shop;
    }
    #endregion

    #region Opinion
    //Note: 'I opine' vs 'It is (opine)'
    public static string IFeelAboutThem(Citizen speaker, Citizen subject, bool isLying)
    {
        RefreshVoice(speaker);

        bool assert = Linguistics.Assert(voice.assertion);
        bool direct = Linguistics.Assert(voice.distance);

        string opinion = speaker.socialCircle[subject.Name].opinion;

        //I -- them/they are --
        string iFeelAboutThem = "";

        if (direct)
        {
            iFeelAboutThem = string.Format("{0} {1} {2}{3}",
                Linguistics.Me(voice, true),                                //0
                opinion,                                                    //1
                Linguistics.Gender(subject, false),                         //2
                Linguistics.Energize(speaker.personality.energized, assert) //3
            );
        }
        else
        {
            iFeelAboutThem = string.Format("{0}{2} {1}{3}",
                Linguistics.Gender(subject, true),                                  //0
                opinion,                                                            //1
                Linguistics.Contract(Linguistics.IS, voice.contraction, isLying),   //2
                Linguistics.Energize(speaker.personality.energized, assert)         //3
            );
        }

        return iFeelAboutThem;
    }
    #endregion

    #region Questline 001
    ///
    #endregion
}
