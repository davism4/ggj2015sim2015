using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainGame : MonoBehaviour {

    public static GameObject manager;

	public static float time; // seconds

    public static float resultsTime = 0f;
    public static bool goingToResults = false;

	public static string GameTitle;
	public static Texture2D ArtTexture;
	public static AudioClip[] AudioSounds = new AudioClip[4];
    public static AudioSource MusicSource;
    
    public static float CodeQuality = 0;
    public static float ArtQuality = 0;
    public static float AudioQuality = 0;
    public static float DesignQuality = 0;
    public static float QualityQuality = 0;

    public void Reset()
    {
        GameTitle = "Your Title Here";
        CodeQuality = 0;
        ArtQuality = 0;
        AudioQuality = 0;
        DesignQuality = 0;
        QualityQuality = 0;
        time = 48f;
    }

    void Start()
    {
        if (manager == null)
        {
            manager = this.gameObject;
            MusicSource = GetComponent<AudioSource>();
            MusicSource.Play();
            Reset();
        }
        else if (this.gameObject != manager)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    static Texture2D timerTexture;
    static GUIStyle timerStyle;

    void Update()
	{
        //if (time > 0f && time < 6f && CodeQuality > 0 && AudioQuality > 0 && DesignQuality > 0 && ArtQuality > 0)
        //{
        //    if (Application.loadedLevelName != "QAScene")
        //        Application.LoadLevel("QAScene");
        //}
        if (time > 0)
            time -= Time.deltaTime;
        else
            time = 0f;

        if (goingToResults)
        {
            resultsTime -= Time.deltaTime;
            if (resultsTime <= 0f)
            {
                resultsTime = float.MaxValue; // I literally give 0 fucks
                Application.LoadLevel("EndScene2");
            }
        }
	}
    
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.Backspace))
        {
            time = 1f;
        }
    }

    public static void StartResultsTransition()
    {
        goingToResults = true;
        resultsTime = 4f;
    }



}
