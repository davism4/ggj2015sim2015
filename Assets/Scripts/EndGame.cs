using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGame : MonoBehaviour {

    public Text txtArt;
    public Text txtCode;
    public Text txtDesign;
    public Text txtMusic;
    public Text txtQA;
    public Text txtTitle;
    public Button ResetButton;
    
    enum States { Load, Play, Crash };
    States state;

    new AudioSource audio;

    float ftimer;
    int soundIndex = 0;

    // Sounds found at SoundBible.com

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        ftimer = 1f;
        state = States.Load;
        txtTitle.text = MainGame.GameTitle;
        if (MainGame.MusicSource != null)
            MainGame.MusicSource.Stop();
    }

    void Update()
    {
        if (state == States.Load)
        {
            ftimer -= Time.deltaTime;
            if (ftimer <= 0)
            {
                ftimer = 1f;
                state = States.Play;
            }
        }
        else if (state == States.Play)
        {
            if (!audio.isPlaying)
            {
                audio.PlayOneShot(MainGame.AudioSounds[soundIndex]);
                soundIndex = (soundIndex + 1) % MainGame.AudioSounds.Length;
            }
        }
    }
}
