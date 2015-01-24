using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLevel : MonoBehaviour
{
    public SpriteRenderer[] topRenderers; // good/bad picture
    public SpriteRenderer[] instrumentRenderers; // instrument picture

    public AudioClip[] Clips = new AudioClip[4];
    public Sprite[] InsturmentSprites = new Sprite[4];
    public Sprite[] MarkSprites = new Sprite[2];

    int Length;

    bool[] correct;
    int[] patternCorrect;
    int[] patternEntered;

    int numPlayed;
    int numEntered;
    float instrumentTimer;
    float instrumentPlaytime = 1f; // how long each demo sound plays

    public void EnterSound(InstrumentButton i)
    {
        if (numEntered < Length)
        {
            MainGame.AudioSounds[numEntered] = i.audioSource.clip;
            i.audioSource.PlayOneShot(i.audioSource.clip);
            patternEntered[numEntered] = i.instrumentType;
            correct[numEntered] = (bool)(patternCorrect[numEntered] == patternEntered[numEntered]);
            
            topRenderers[numEntered].sprite = MarkSprites[correct[numEntered] ? 0 : 1];

            Debug.Log("Entered " + i.ToString() + ": " + correct[numEntered].ToString());
            numEntered++;
        }
    }

    void PlayInstrument()
    {
        numPlayed++;
    }

    void SetMusicQuiet()
    {

    }

    void SetMusicLoud()
    {
    }

    void OnDisable()
    {
        SetMusicQuiet();
    }

    void SetPattern()
    {
        for (int i = 0; i < Length; i++)
        {
            int r = UnityEngine.Random.Range(0, 4);
            patternCorrect[i] = r;
            instrumentRenderers[i].sprite = InsturmentSprites[r];
        }
    }
    
    void Awake() // once
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("MusicNode");
        Length = nodes.Length;
        Transform[] children = new Transform[Length];
        children[0] = transform.Find("MusicNode 0");
        children[1] = transform.Find("MusicNode 1");
        children[2] = transform.Find("MusicNode 2");
        children[3] = transform.Find("MusicNode 3");
        //child = new Transform[transform.childCount];
        
        topRenderers = new SpriteRenderer[Length];
        instrumentRenderers = new SpriteRenderer[Length];
        patternCorrect = new int[Length];
        patternEntered = new int[Length];
        correct = new bool[Length];
        
        for (int i = 0; i < Length; i++)
        {
            instrumentRenderers[i] = children[i].GetComponent<SpriteRenderer>();
            topRenderers[i] = instrumentRenderers[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        MainGame.AudioSounds = new AudioClip[Length];
        //DontDestroyOnLoad(transform.gameObject);
        SetPattern();
    }

    void Start()
    {
        numPlayed = 0;
        numEntered = 0;
        instrumentTimer = 0f;
        // make correct pattern
    }

    void Update()
    {
        if (numPlayed < Length)
        {
            if (instrumentTimer > 0f)
            {
                instrumentTimer -= Time.deltaTime;
            }
            else
            {
                numPlayed++;
                instrumentTimer = instrumentPlaytime;
            }
        }
    }

    void LateUpdate()
    {
        if (numEntered == Length)
        {
            float goodcount = 0;

            for (int i = 0; i < Length; i++)
            {
                if (patternCorrect[i] == patternEntered[i])
                {
                    goodcount += 1f;
                }
            }
            MainGame.AudioQuality = goodcount / Length;
            Application.LoadLevel("GameMenuScene");
        }
    }
}