using UnityEngine;
using UnityEditor;
using System.Collections;

public class MainGame : MonoBehaviour {

	public static float time; // seconds
	
	public static string GameTitle;
	public static Texture2D ArtTexture;
	public static AudioClip[] AudioSounds;

    public static float CodeQuality = 0;
    public static float ArtQuality = 0;
    public static float AudioQuality = 0;
    public static float DesignQuality = 0;
    public static float QualityQuality = 0;
	
	public static int IndexSceneStart = 0;
    public static int IndexSceneInstructions = 1;
    public static int IndexSceneGameMenu = 2;
    public static int IndexSceneArt = 3;
    public static int IndexSceneCode = 4;
    public static int IndexSceneDesign = 5;
    public static int IndexSceneMusic = 6;
    public static int IndexSceneEnd = 7;

    void Update()
	{
		time -= Time.deltaTime;
	}

}
