using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioClip))]
[RequireComponent(typeof(Button))]
public class InstrumentButton : MonoBehaviour
{
    [HideInInspector] public int instrumentType;
    public AudioClip clip;

    public enum Instrument : int { Trumpet, Guitar, Bell, Drum };
    public Instrument instrument;

    void Awake()
    {
        if (instrument == Instrument.Trumpet) instrumentType = 0;
        else if (instrument == Instrument.Guitar) instrumentType = 1;
        else if (instrument == Instrument.Bell) instrumentType = 2;
        else instrumentType = 3;
    }

    ////Rect rect;
    //Texture texture;
    //[HideInInspector] public new Transform transform;
    //MusicLevel musiclevel;

    //void Awake()
    //{
    //    transform = GetComponent<Transform>();
        
    //    //DontDestroyOnLoad(transform.gameObject);
    //  //  clip = GetComponent<AudioClip>();
    //    Vector3 center = Camera.main.WorldToScreenPoint(transform.position);
    //    //rect = GetComponent<SpriteRenderer>().sprite.rect;
    //    //rect = new Rect(
    //    //    center.x - transform.localScale.x*rect.width/4,
    //    //    Screen.height-center.y-transform.localScale.y*rect.height/4,
    //    //    transform.localScale.x*rect.width/2,
    //    //    transform.localScale.y*rect.height/2
    //    //);
    //    //texture = GetComponent<SpriteRenderer>().sprite.texture;
    //    musiclevel = GameObject.FindObjectOfType<MusicLevel>();

    //   // audioSource.clip = musiclevel.Clips[instrumentType];

    //    //GetComponent<SpriteRenderer>().enabled = false;
    //    //rect = new Rect(0, 0, 90, 90);
    //    //rect = GetComponent<GUITexture>().GetScreenRect();
    //}

    //void OnGUI()
    //{
    //    //if (GUI.Button(new Rect(0, 0, 90, 90), "TEXTURE"))
    //    //{
    //    //    Debug.Log("PRESSED BUTTON " + name);
    //    //}'
    //    if (Application.loadedLevelName == "MusicScene")
    //    {
    //        if (musiclevel.CanClick)
    //        {
    //            GUI.contentColor = Color.white;
    //        }
    //        else
    //        {
    //            GUI.contentColor = Color.gray;
    //        }
    //        if (GUI.Button(rect, texture))
    //        {
    //            if (musiclevel.CanClick)
    //                musiclevel.EnterSound(this);
    //        }
    //    }
    //}

}