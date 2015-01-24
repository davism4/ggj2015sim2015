using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLevel : MonoBehaviour
{
    SpriteRenderer[] topRenderers; // good/bad picture
    SpriteRenderer[] instrumentRenderers; // instrument picture
    new AudioSource audio;

    public AudioClip[] Clips = new AudioClip[4];
    public Sprite[] InsturmentSprites = new Sprite[5];
    public Sprite[] MarkSprites = new Sprite[2];

    int Length;

    public bool[] correct;
    public int[] patternCorrect;
    public int[] patternEntered;

    public int numEntered;
    public int playIndex;
    public float instrumentTimer;

    public void EnterSound(InstrumentButton i)
    {
        if (numEntered < Length)
        {
            MainGame.AudioSounds[numEntered] = i.audioSource.clip;
            i.audioSource.PlayOneShot(i.audioSource.clip);
            patternEntered[numEntered] = i.instrumentType;
            correct[numEntered] = (bool)(patternCorrect[numEntered] == patternEntered[numEntered]);
            instrumentRenderers[numEntered].sprite = InsturmentSprites[patternCorrect[numEntered]];
            topRenderers[numEntered].sprite = MarkSprites[correct[numEntered] ? 0 : 1];

            //Debug.Log("Entered " + i.ToString() + ": " + correct[numEntered].ToString());
            numEntered++;
        }
    }

    void OnDisable()
    {
        MainGame.MusicSource.volume = 1f;
    }

    void SetPattern()
    {
        for (int i = 0; i < Length; i++)
        {
            int r = UnityEngine.Random.Range(0, 4);
            patternCorrect[i] = r;
            instrumentRenderers[i].sprite = null;// InsturmentSprites[4];
            //instrumentRenderers[i].enabled = false;
        }
    }
    
    void Awake() // once
    {
        audio = GetComponent<AudioSource>();
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("MusicNode");
        Length = nodes.Length;
        Transform[] children = new Transform[Length];
        children[0] = transform.Find("MusicNode 0");
        children[1] = transform.Find("MusicNode 1");
        children[2] = transform.Find("MusicNode 2");
        children[3] = transform.Find("MusicNode 3");
        
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

    }

    void Start()
    {
        SetPattern();
        numEntered = 0;
        playIndex = 0;
        instrumentTimer = 0;
        MainGame.MusicSource.volume = 0.25f;
    }

    void Update()
    {
        if (playIndex <= 4)
        {
            if (instrumentTimer > 0f)
            {
                instrumentTimer -= Time.deltaTime;
            }
            else
            {
                instrumentTimer = 1.0f;
                audio.Stop();
                if (playIndex > 0)
                {
                    instrumentRenderers[playIndex-1].sprite = null;
                    
                }
                if (playIndex < 4)
                {
                    instrumentRenderers[playIndex].sprite = InsturmentSprites[4];//patternCorrect[playIndex]];
                    audio.PlayOneShot(Clips[patternCorrect[playIndex]]);
                    playIndex++;
                }
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