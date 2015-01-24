using UnityEngine;
using UnityEditor;
using System.Collections;

public class MainGame : MonoBehaviour {

    public static GameObject manager;

	public static float time; // seconds
    public static bool AtQA = false;

	public static string GameTitle;
	public static Texture2D ArtTexture;
	public static AudioClip[] AudioSounds;
	
    public static AudioSource MusicSource;
    
    public static float CodeQuality = 0;
    public static float ArtQuality = 0;
    public static float AudioQuality = 0;
    public static float DesignQuality = 0;
    public static float QualityQuality = 0;

    void Start()
    {
        if (CodeQuality > 0 && AudioQuality > 0 && DesignQuality > 0 && ArtQuality > 0 && !AtQA)
        {
            AtQA = true;
            Application.LoadLevel("QAScene");
        }

        if (manager == null)
        {
            manager = this.gameObject;
            MusicSource = GetComponent<AudioSource>();
            MusicSource.Play();
        }
        if (this.gameObject != manager)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    static Texture2D timerTexture;
    static GUIStyle timerStyle;

    void Awake() // once
    {
        DontDestroyOnLoad(transform.gameObject);
        timerTexture = Resources.Load<Texture2D>("Art/timerBase");
    }

    void Update()
	{
		
	}

    public static void StartGame()
    {
        time = 48f;
        // Application.LoadLevel(IndexSceneDesign);
        
    }

    
    public static void Tick()
    {
        if (time > 0)
            time -= Time.deltaTime;
        else
            time = 0f;
        // calculate
        float panicScale = 1 - (time / 48);
        float offsetx = 0;//panicScale * Mathf.Cos(time+(5*panicScale) ) * 0.5f; // shaking?
        float offsety = 0;//panicScale * Mathf.Sin(time+(4*panicScale) ) * 1;
        timerStyle = GUI.skin.GetStyle("Label");
        timerStyle.alignment = TextAnchor.MiddleLeft;
        // draw
        GUI.Label(new Rect(5 + offsetx, 5 + offsety, 110, 110), timerTexture);
        GUI.contentColor = new Color(panicScale, 0, 0);
        GUI.skin.label.fontSize = Mathf.RoundToInt(15 + 3 * panicScale);
        GUI.Label(new Rect(24, 42, 55, 55),
                time > 0f? time.ToString("##.##"): "0.00", timerStyle);

        if (time <= 0 && Application.loadedLevelName != "MainGameScene")
        {
            Debug.Log("TIME'S UP!");
            Application.LoadLevel("GameMenuScene");
        }
        
    }

    void OnGUI()
    {
        Tick();
        if (true)//Application.loadedLevel == "GameMenuScene")
        {
            if (GUI.Button(new Rect(Screen.width * 0.33f, Screen.height * 0.33f, Screen.width * 0.33f, Screen.height * 0.33f), "DESIGN"))
            {
                Application.LoadLevel("DesignScene");
            }
            else if (GUI.Button(new Rect(Screen.width * 0.66f, Screen.height * 0.33f, Screen.width * 0.33f, Screen.height * 0.33f), "CODE"))
            {
                Application.LoadLevel("CodeScene");
            }
            else if (GUI.Button(new Rect(Screen.width * 0.33f, Screen.height * 0.66f, Screen.width * 0.33f, Screen.height * 0.33f), "ART"))
            {
                Application.LoadLevel("ArtScene");
            }
            else if (GUI.Button(new Rect(Screen.width * 0.66f, Screen.height * 0.66f, Screen.width * 0.33f, Screen.height * 0.33f), "MUSIC"))
            {
                Application.LoadLevel("MusicScene");
            }
        }
    }
}
