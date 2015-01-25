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
    AudioSource badAudio;

    float ftimer;
    public int soundIndex = 0;

    // Sounds found at SoundBible.com

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        badAudio = transform.GetChild(0).GetComponent<AudioSource>();
    }

    void Start()
    {
        ftimer = 1f;
        state = States.Load;
        txtTitle.text = MainGame.GameTitle;
        if (MainGame.MusicSource != null)
            MainGame.MusicSource.Stop();
        Transform clock = GameObject.FindObjectOfType<Timer>().transform;
        if (clock!=null)
            Destroy(clock.gameObject);
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
            if (MainGame.AudioSounds.Length>0 && !audio.isPlaying)
            {
                audio.clip = MainGame.AudioSounds[soundIndex];
                audio.Play();
                soundIndex = (soundIndex + 1) % MainGame.AudioSounds.Length;
                if (UnityEngine.Random.Range(0f, 1f) > MainGame.AudioQuality)
                {
                    badAudio.clip = MainGame.AudioSounds[(soundIndex + 2) % MainGame.AudioSounds.Length];
                    badAudio.PlayDelayed(0.6f);
                }
            }
        }
    }
}
