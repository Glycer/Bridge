using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Linguistics
{
    public static bool Assert(float assertiveness)
    {
        return Random.Range(0f, 1f) < assertiveness;
    }

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
}
