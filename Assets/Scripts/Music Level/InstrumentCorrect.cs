using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class InstrumentCorrect : MonoBehaviour
{
    public AudioSource audiosource;
    public SpriteRenderer renderer;
    [HideInInspector] public new Transform transform;

    void Awake()
    {
        transform = GetComponent<Transform>();
        audiosource = GetComponent<AudioSource>();
    }

}