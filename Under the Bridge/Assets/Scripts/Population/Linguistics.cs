using System.Collections.Generic;
using UnityEngine;

public static class Linguistics
{
    public static Dictionary<string, string[]> firstThird = new Dictionary<string, string[]> {
        { "am", new string[] { "am", "is" } },
        { "do", new string[] { "do", "does" } }
    };

    public const string ARE = "are", HAD = "had", IS = "is", NOT = "not", IS_IT_NOT = "is it not", WOULD = "would";
    public static string ARE_NOT = "are not", IS_NOT = "is not", WOULD_NOT = "would not";

    static Dictionary<string, string> contractions = new Dictionary<string, string> {
        { ARE, "'re" },
        { HAD, "'d" },
        { IS, "'s" },
        { NOT, "n't" },
        { IS_IT_NOT, " isn't it" },
        { WOULD, "'d" }
    };

    static Dictionary<string, string[]> doubleContractions = new Dictionary<string, string[]> {
        { ARE_NOT, new string[] { "'re not", " aren't" } },
        { IS_NOT, new string[] { "'s not", " isn't" } },
        { WOULD_NOT, new string[] { "'d not", " wouldn't" } }
    };

    public static bool Assert(float assertiveness)
    {
        return Random.Range(0f, 1f) < assertiveness;
    }

    /// <summary>
    /// Capitalizes the first letter of a word.
    /// </summary>
    /// <param name="str">The word to capitalize.</param>
    /// <returns>String: The given word, capitalized.</returns>
    public static string Capitalize(string str)
    {
        return str != "" ? char.ToUpper(str[0]) + str.Substring(1) : "";
    }

    /// <summary>
    /// Add punctuation to a string. Indexes the string to an array of individual words.
    /// Array marks must be the same length as array indices
    /// </summary>
    /// <param name="marks">Punctuation marks</param>
    /// <param name="indices">Indices of the words</param>
    /// <returns>The punctuated string</returns>
    public static string Punctuate(string str, char[] marks, int[] indices)
    {
        string temp = "";
        string[] strArray = str.Split(' ');

        for (int i = 0; i < marks.Length; i++)
        {
            strArray[indices[i]] += marks[i];

            if (i > 0)
            {
                char prev = marks[i - 1];
                if (prev == '.' || prev == '!' || prev == '?')
                    strArray[indices[i]] = Capitalize(strArray[indices[i]]);
            }
        }

        for (int i = 0; i < strArray.Length - 1; i++)
            temp += strArray[i] + " ";

        return temp + strArray[strArray.Length-1];
    }

    /// <summary>
    /// Determines the energy of a sentence.
    /// </summary>
    /// <returns>".", "!", "?", or "?!"</returns>
    public static string Energize(bool energized, bool assertion)
    {
        return energized ?
            assertion ? "!" : "?!" :
            assertion ? "." : "?";
    }

    /// <summary>
    /// Enter gender into a sentence.
    /// </summary>
    /// <param name="subject">The citizen being referenced.</param>
    /// <param name="isSubject">Is the subject the sentence's subject?</param>
    /// <returns>String: the subject's gender in the correct case.</returns>
    public static string Gender(Citizen subject, bool isSubject)
    {
        return subject.isMale ?
            isSubject ? "he" : "him" :
            isSubject ? "she" : "her";
    }

    /// <summary>
    /// Enter self-reference into a sentence.
    /// </summary>
    /// <param name="speaker">The voice speaking.</param>
    /// <param name="isSubject">Is the speaker the sentence's subject?</param>
    /// <returns>String: I, me, or the speaker's name.</returns>
    public static string Me(Voice speaker, bool isSubject)
    {
        if (speaker.iAmMe)
            return isSubject ? "I" : "me";
        else
            return speaker.name;
    }

    /// <summary>
    /// Determines whether to contract or not.
    /// </summary>
    /// <param name="contractionRange">Voice contraction variable.</param>
    /// <param name="isLying">Is the speaker lying?</param>
    /// <returns>Bool</returns>
    static bool Contract(float contractionRange, bool isLying)
    {
        if (contractionRange < .2f || (contractionRange < .8f && isLying))
            return false;
        else if (contractionRange > .4f)
            return true;
        else
            return Random.Range(0, 2) > 0;
    }

    /// <summary>
    /// Enter a contractable word(s) into a sentence.
    /// </summary>
    /// <param name="contractee">The contractable word(s).</param>
    /// <param name="contractionRange">The voice's tendency to contract (0-1).</param>
    /// <param name="isLying">Is the speaker lying?</param>
    /// <returns>String: the word(s) either contracted or unchanged.</returns>
    public static string Contract(string contractee, float contractionRange, bool isLying)
    {
        if (Contract(contractionRange, isLying))
        {
            if (doubleContractions.ContainsKey(contractee))
                return doubleContractions[contractee][Random.Range(0, 2)];
            else
                return contractions.ContainsKey(contractee) ? contractions[contractee] : " " + contractee;
        }
        else
            return " " + contractee;
    }
}
