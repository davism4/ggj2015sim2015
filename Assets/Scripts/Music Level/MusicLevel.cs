using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicLevel : MonoBehaviour
{
    public AudioClip[] SoundTypes;
    public Texture2D[] Instruments;

    void Start()
    {
        if (SoundTypes.Length == 0)
        {
            SoundTypes = Resources.LoadAll<AudioClip>("Sound");
        }
        if (Instruments.Length != SoundTypes.Length)
        {
            Instruments = Resources.LoadAll<Texture2D>("Art/Instruments");
        }
    }

}