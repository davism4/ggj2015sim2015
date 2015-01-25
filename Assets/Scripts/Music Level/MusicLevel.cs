using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicLevel : MonoBehaviour
{
    public AudioClip[] Clips = new AudioClip[4];
    public Sprite[] InsturmentSprites = new Sprite[5];
    public Sprite[] MarkSprites = new Sprite[2];

    float MainMusicVolumeReduction = 0.3f;
    
    new AudioSource audio;

    int Length;
    bool[] correct;
    public int[] patternCorrect;
    public int[] patternEntered;
    Image[] markImages; // good/bad picture
    Image[] instrumentImages; // instrument picture
    int numEntered;
    int playIndex;
    float instrumentTimer;
    float clickdelay = 0f;
    public bool CanClick { get { return (playIndex > 4) && (clickdelay <= 0f); } }

    public GameObject[] buttons;

    public void EnterSound(InstrumentButton i)
    {
        if (numEntered < Length)
        {
            MainGame.AudioSounds[numEntered] = i.clip;
            audio.clip = i.clip;
            audio.Play();
            patternEntered[numEntered] = i.instrumentType;
            correct[numEntered] = (bool)(patternCorrect[numEntered] == patternEntered[numEntered]);
            instrumentImages[numEntered].enabled = true;
            Debug.Log(patternCorrect[numEntered]);
            instrumentImages[numEntered].sprite = InsturmentSprites[patternCorrect[numEntered]];
            markImages[numEntered].enabled = true;
            markImages[numEntered].sprite = MarkSprites[correct[numEntered] ? 0 : 1];
            numEntered++;
            clickdelay = 0.98f; // forced to wait for most of the sound to play
        }
    }

    void OnDestroy()
    {
        if (MainGame.MusicSource != null)
            MainGame.MusicSource.volume = 1f;
        int c = 0;
        for (int i = 0; i < Length; i++)
        {
            if (correct[i]) c++;
        }
        MainGame.AudioQuality = (float)c / Length;
    }

    void SetPattern()
    {
        for (int i = 0; i < Length; i++)
        {
            int r = UnityEngine.Random.Range(0, 4);
            patternCorrect[i] = r;
            instrumentImages[i].sprite = null;// InsturmentSprites[4];
            instrumentImages[i].enabled = false;
            markImages[i].sprite = null;
            markImages[i].enabled = false;
            //instrumentImages[i].enabled = false;
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

        markImages = new Image[Length];
        instrumentImages = new Image[Length];
        patternCorrect = new int[Length];
        patternEntered = new int[Length];
        correct = new bool[Length];

        //Button[] b = GameObject.FindObjectsOfType<Button>();
        buttons = GameObject.FindGameObjectsWithTag("InstrumentButton");

        for (int i = 0; i < Length; i++)
        {
            instrumentImages[i] = children[i].GetComponent<Image>();
            markImages[i] = instrumentImages[i].transform.GetChild(0).GetComponent<Image>();
            //buttons[i] = b[i].gameObject;
            buttons[i].SetActive(false);
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
        if (MainGame.MusicSource != null)
            MainGame.MusicSource.volume = MainMusicVolumeReduction;
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
                    instrumentImages[playIndex - 1].sprite = null;
                    instrumentImages[playIndex - 1].enabled = false;

                }
                if (playIndex < 4)
                {
                    instrumentImages[playIndex].enabled = true;
                    instrumentImages[playIndex].sprite = InsturmentSprites[4];//patternCorrect[playIndex]];
                    audio.PlayOneShot(Clips[patternCorrect[playIndex]]);
                }
                playIndex++;
            }
        }
        else if (playIndex == 5)
        {
            for (int i = 0; i < 4; i++)
            {
                buttons[i].SetActive(true);
            }
            playIndex++;
        }
        if (clickdelay >= 0f) clickdelay -= Time.deltaTime;

    }

    void LateUpdate()
    {
        if (numEntered == Length && clickdelay<=0f)
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