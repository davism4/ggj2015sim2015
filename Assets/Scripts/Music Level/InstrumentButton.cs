using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class InstrumentButton : MonoBehaviour
{
    public AudioSource audioSource;
    public MusicLevel.Instrument instrumentType;

    Rect rect;
    Texture texture;
    [HideInInspector] public new Transform transform;
    MusicLevel musiclevel;

    void Awake()
    {
		transform = GetComponent<Transform>();
        //DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
        Vector3 center = Camera.main.WorldToScreenPoint(transform.position);
        rect = GetComponent<SpriteRenderer>().sprite.rect;
        rect = new Rect(
            center.x - transform.localScale.x*rect.width/4,
            Screen.height-center.y-transform.localScale.y*rect.height/4,
            transform.localScale.x*rect.width/2,
            transform.localScale.y*rect.height/2
        );
        texture = GetComponent<SpriteRenderer>().sprite.texture;
        musiclevel = GameObject.FindObjectOfType<MusicLevel>();
        switch (instrumentType)
        {
            case MusicLevel.Instrument.Bell:
                audioSource.clip = MusicLevel.ClipBell;
                break;
            case MusicLevel.Instrument.Drum:
                audioSource.clip = MusicLevel.ClipDrum;
                break;
            case MusicLevel.Instrument.Guitar:
                audioSource.clip = MusicLevel.ClipGuitar;
                break;
            default:
                audioSource.clip = MusicLevel.ClipTrumpet;
                break;
        }
        //GetComponent<SpriteRenderer>().enabled = false;
        //rect = new Rect(0, 0, 90, 90);
        //rect = GetComponent<GUITexture>().GetScreenRect();
    }

    void OnGUI()
    {
        //if (GUI.Button(new Rect(0, 0, 90, 90), "TEXTURE"))
        //{
        //    Debug.Log("PRESSED BUTTON " + name);
        //}'
        if (Application.loadedLevelName=="MusicScene")
        if (GUI.Button(rect, texture))
        {
            Debug.Log("Clicked the button with text");
        }
    }

}