using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Reviews : MonoBehaviour
{
    [HideInInspector] public new Transform transform;
    public Text text;
    public GameObject ResetButton;

    string message;
    int charcount;
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

    public enum States { Waiting, Entering, Typing, Leaving, Done };
    public States state;
    float delay = 0f;

    float stopHeight, endHeight;
    public float slideSpeed = 50f;
    public float waitTime = 3f;

    static bool Chance(float percent)
    {
        if (UnityEngine.Random.Range(0f, 1f) < percent)
            return true;
        else
            return false;
    }

    void Awake()
    {
        transform = GetComponent<Transform>();
        state = States.Entering;
        stopHeight = Screen.height * 0.5f;
        endHeight = Screen.height * 3;
        
        Reset();
    }

    void Update()
    {
        if (state == States.Entering)
        {
            if (transform.position.y < stopHeight)
                transform.position += Vector3.up * slideSpeed * Time.deltaTime;
            else
                state = States.Typing;
        }
        else if (state == States.Leaving)
        {
            if (transform.position.x < endHeight)
                transform.position += Vector3.up * slideSpeed * Time.deltaTime;
            else
                state = States.Done;
        }
        else if (state == States.Waiting)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                state = States.Leaving;
                ResetButton.SetActive(true);
            }
        }
        else if (state == States.Typing)
        {
            if (charcount < message.Length)
            {
                if (delay > 0)
                {
                    delay -= Time.deltaTime;
                }
                else
                {
                    delay = 0.005f;
                    text.text += message[charcount];
                    charcount++;
                }
            }
            else
            {
                delay = waitTime;
                state = States.Waiting;
            }
        }
    }

    void Start()
    {
        text.text = "";
        message = "";
        charcount = 0;
        state = States.Entering;
        for (int i = 0; i < 3; i++)
        {
            message += GenerateReview() + "\n";
        }
        message += GenerateReview();
    }

    void Reset()
    {
        ResetButton.SetActive(false);
        authors = new List<string>() { "T. Cordle", "M. Davis", "C. Hamer", "A. Wozniak" };
        subjects = new List<int>() { 0, 1, 2, 3 };
        verbs = new List<string>()
        {
            "commented", "comments", "says", "said", "wrote", "writes", "blogged", "tweeted",
        };
        prefixes = new List<string>()
        {
           "Audiences will think the",
           "Audiences will say the",
           "Critics say the",
           "Critics agree the",
           "I believe that the",
           "I believe the",
           "I feel that the",
           "I feel the",
           "I found that the",
           "I think that the",
           "I think the",
           "I thought that the",
           "I'd say that the",
           "IGN says the",
           "In my opinion, the",
           "Its","Its","Its",
           "Judges believe the",
           "Judges said the",
           "Reviews agree the",
           "Reviewers say the",
           "The","The","The",
           "The game's",
           "This game's",
           "Unlike IGN, I think the",
        };
        terms_art = new List<string>()
        {
            "art", "art style", "artwork",
            "backgrounds",
            "character models", "cinematics",
            "graphics",
            "imagery",
            "models",
            "resolution",
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
        adverbs_low = new List<string>() {
           "","","","","",
           "a bit", "almost", "arguably",
           "fairly",
           "kind of",
           "maybe", "marginally", "moderately", "more or less",
           "occasionally",
           "partially", "probably", "possibly",
           "rather", "reasonably", "relatively",
           "seemingly", "slightly", "somewhat",
        };
        adverbs_high = new List<string>() {
           "clearly",
           "definitely",
           "exceptionally", "extremely",
           "greatly",
           "immensely", "incredibly",
           "notably",
           "overly",
           "quite",
           "remarkably",
           "so",
           "uncommonly", "undeniably",
           "very",
        };
        adjectives_good = new List<string>(){
           "acceptable", "admirable",
           "creative",
           "decent", "delightful",
           "enjoyable", "excellent",
           "good", "great",
           "fantastic", "fun",
           "high-quality",
           "nice",
           "okay",
           "pleasant",
           "satisfying", "sophisticated", "superb",
           "unique", 
           "valuable", "vivid",
           "well done", "worthy",
        };
        adjectives_bad = new List<string>()
        {
           "atrocious", "annoying", "awful",
           "bad", "boring", "broken",
           "cheap", "cheesy", "childish",
           "depressing", "distasteful", "dreadful", "dull",
           "flawed", "foul", "frustrating",
           "hideous", "horrendous", "horrible",
           "irritating",
           "lame", "lousy",
           "mediocre", "miserable",
           "noxious",
           "obscene", "offensive", "ordinary",
           "terrible",
           "pathetic", "pitiable", "poor",
           "repetitive", "rough",
           "sad", "shabby", "sorry", "sub-par",
           "uncreative", "unpleasant",
           "vile",
           "worthless"
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
        string text = author + " " + verb + ": ";

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
            text += Chance(0.5f) ? "are" : "were";
        else
            text += Chance(0.5f) ? "is" : "was";
        text += " ";
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
        text += Chance(0.8f) ? "." : "!";
        text += "\" (" + (quality*10).ToString("0.#") + "/10)";
        text = text.Replace("  ", " ");
        return text;
    }
}
