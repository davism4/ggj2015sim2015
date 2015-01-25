using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Reviews : MonoBehaviour
{
    public Text text;
    List<string> prefixes;
    List<string> adverbs_low;
    List<string> adverbs_high;
    List<string> adjectives_good;
    List<string> adjectives_bad;
    List<string> terms_sound;
    List<string> terms_art;
    List<string> terms_code;
    List<string> terms_design;
    List<string> authors;
    List<int> subjects;
    List<string> verbs;

    static bool Chance(float percent)
    {
        if (UnityEngine.Random.Range(0f, 1f) < percent)
            return true;
        else
            return false;
    }

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        text.text = "";
        for (int i = 0; i < 3; i++)
        {
            text.text += GenerateReview() + "\n";
        }
        text.text += GenerateReview();
    }

    void Reset()
    {
        authors = new List<string>() { "T. Cordle", "M. Davis", "C. Hamer", "A. Wozniak" };
        subjects = new List<int>() { 0, 1, 2, 3 };
        verbs = new List<string>()
        {
            "says", "said", "wrote", "writes", "blogged", "tweeted"
        };
        terms_art = new List<string>()
        {
            "art", "art style", "artwork",
            "backgrounds",
            "character models", "cinematics",
            "graphics",
            "imagery",
            "models",
            "scenery",
            "visuals"
        };
        terms_sound = new List<string>()
        {
            "arrangement", "audio",
            "composition",
            "music",
            "songs", "sound", "soundtrack",
            "theme song",
            "voice acting",
        };
        terms_design = new List<string>()
        {
            "character development",
            "concept",
            "design", "dialogue",
            "idea",
            "plot",
            "story",
            "writing",
        };
        terms_code = new List<string>()
        {
            "code", "controls",
            "DLC",
            "engine",
            "framerate",
            "loading time",
            "menus",
            "physics",
            "response time",
            "stability"
        };
        prefixes = new List<string>()
        {
           "Critics say that the",
           "I liked how the",
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
           "fantastic",
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
           "awful",
           "bad",
           "childish",
           "distasteful",
           "flawed",
           "horrendous",
           "mediocre",
           "terrible",
           "poor","poorly done",
           "unpleasant",
           "vile",
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

    int PullInt(List<int> list)
    {
        int i = UnityEngine.Random.Range(0, list.Count - 1);
        int pulled = list[i];
        list.RemoveAt(i);
        return pulled;
    }

    string GenerateReview()
    {
        int subject = PullInt(subjects);
        string author = PullWord(authors);
        string verb = PullWord(verbs);
        string text = author + " " + verb + " ";

        if (subject == 0)
            text += GenerateSentence(PullWord(terms_art), MainGame.ArtQuality);
        else if (subject == 1)
            text += GenerateSentence(PullWord(terms_code), MainGame.CodeQuality);
        else if (subject == 2)
            text += GenerateSentence(PullWord(terms_design), MainGame.DesignQuality);
        else
            text += GenerateSentence(PullWord(terms_sound), MainGame.AudioQuality);
        return text;
    }

    string GenerateSentence(string subject, float quality)
    {
        string text ="\"";
        // prefix + subject
        text += PullWord(prefixes)  + " " + subject + " ";
        if (subject[subject.Length - 1] == 's')
            text += Chance(0.5f) ? "are " : "were ";
        else
            text += Chance(0.5f) ? "is " : "was ";
        
        // adverb
        if (quality <= 0.25f)
        {
            text += PullWord(adverbs_high) + " " + PullWord(adjectives_bad);
        }
        else if (quality <= 0.5f)
        {
            text += PullWord(adverbs_low) + " " + PullWord(adjectives_bad);
        }
        else if (quality <= 0.75f)
        {
            text += PullWord(adverbs_low) + " " + PullWord(adjectives_good);
        }
        else
        {
            text += PullWord(adverbs_high) + " " + PullWord(adjectives_good);
        }
        text += Chance(.5f) ? "." : "!";
        text += "\" (" + (quality*10).ToString("0.0") + "/10)";
        return text;
    }
}
