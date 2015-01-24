using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {

	public float time; // seconds
	public int IntTime {
		get { return Mathf.RoundToInt(Time); }
	}
	
	public string GameTitle;
	public Texture2D ArtTexture;
	public AudioClip[] AudioSounds;
	
	public float ArtQuality = 0f;
	public float AudioQuality = 0f;
	public float StringQuality = 0f;
	
	void Awake()
	{
		time = 48f;
	}
	
	void Update()
	{
		time -= Time.deltaTime;
	}
}
