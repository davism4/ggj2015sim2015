using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Reviews : MonoBehaviour
{

    List<string> prefixes;
    List<string> adverbs_low;
    List<string> adverbs_high;
    List<string> adjectives_good;
    List<string> adjectives_bad;

    static bool Chance(float percent)
    {
        if (UnityEngine.Random.Range(0f, 1f) < percent)
            return true;
        else
            return false;
    }

    void Reset()
    {
        prefixes = new List<string>()
        {
           "Critics say that the",
           "I thought that the",
           "I'd say the",
           "IGN says the",
           "In my opinion, the",
           "Its",
           "The",
           "This game's",
           "Your mom thinks that the",
        };
        adverbs_low = new List<string>() {
           "",
           "a bit",
           "kind of",
           "moderately",
           "probably",
           "slightly","somewhat",
        };
        adverbs_high = new List<string>() {
           "arguably",
           "clearly",
           "definitely",
           "incredibly",
           "so",
           "very",
        };
        adjectives_good = new List<string>(){
           "admirable",
           "good","great",
           "high-quality",
           "nice",
           "pleasant",
           "superb",
           "unique", 
           "vivid",
           "well done",
        };
        adjectives_bad = new List<string>()
        {
           "bad",
           "awful",
           "terrible",
           "horrendous",
           "vile",
           "flawed",
           "poor","poorly done",
        };
    }

    string PullWord(List<string> list)
    {
        string s;
        int i = UnityEngine.Random.Range(0, list.Count - 1);
        s = list[i];
        list.RemoveAt(i);
        return s;
    }

    string GenerateSentence(string subject, float quality)
    {
        string text ="";
        // prefix + subject
        text += PullWord(prefixes)  + " " + subject + " ";

        // adverb
        if (quality < 0.25f)
        {
            text += PullWord(adverbs_high) + " ";
            text += PullWord(adjectives_bad);
        }
        else if (quality < 0.5f)
        {
        }
        else if (quality < 0.75f)
        {
        }
        else
        {
        }
        text += " (" + Mathf.RoundToInt(quality * 100) + "/10)";
        return text;
    }
}
