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

	void Update()
	{
		time -= Time.deltaTime;
	}

}
