using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLevel : MonoBehaviour
{
    public static GameObject[] iconTypes;
    public static GameObject[] buttons;

    public enum Instrument { Drum, Bell, Guitar, Trumpet };
    public static AudioClip ClipDrum;
    public static AudioClip ClipTrumpet;
    public static AudioClip ClipBell;
    public static AudioClip ClipGuitar;

    int Length;
    Vector3[] positions;
    InstrumentCorrect[] instrumentsCorrect;
    Instrument[] patternCorrect;
    Instrument[] patternEntered;

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
            numEntered++;
        }
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

    void Awake() // once
    {
        //Transform[] children = transform.GetComponentsInChildren<Transform>();
        Length = transform.childCount;
        positions = new Vector3[Length];
        patternCorrect = new Instrument[Length];
        patternEntered = new Instrument[Length];
        instrumentsCorrect = new InstrumentCorrect[Length];
        for (int i = 0; i < Length; i++)
        {
            positions[i] = transform.GetChild(i).position;
        }
        //SoundTypes	= Resources.LoadAll<AudioClip>("Sound");
        //Instruments	= Resources.LoadAll<Texture2D>("Art/Instruments");
        MainGame.AudioSounds = new AudioClip[Length];
        DontDestroyOnLoad(transform.gameObject);
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
            Application.LoadLevel(MainGame.IndexSceneMain);
        }
    }
}