using System;
using System.Collections.Generic;
using UnityEngine;

// Game loads at this screen. Has buttons to (1) read instructions or (2) start game.
public class StartGame : MonoBehaviour
{
    void Start()
    {
        GameObject obj = GameObject.Find("TimerCanvas");
        if (obj != null)
            Destroy(obj);

        PlayerPrefs.SetString("GameTitle", "Game Title Here");
        PlayerPrefs.SetFloat("time", 48f);
        PlayerPrefs.SetInt("CodeQuality", 0);
        PlayerPrefs.SetInt("ArtQuality", 0);
        PlayerPrefs.SetInt("AudioQuality", 0);
        PlayerPrefs.SetInt("DesignQuality", 0);
    }
}
