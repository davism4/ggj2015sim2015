using UnityEngine;
using System.Collections;

public class SceneMgr : MonoBehaviour {

    public void StartCode()
    {
        Application.LoadLevel("CodeScene");
    }

    public void StartArt()
    {
        Application.LoadLevel("ArtScene");
    }

    public void StartSound()
    {
        Application.LoadLevel("MusicScene");
    }

    public void StartDesign()
    {
        Application.LoadLevel("DesignScene");
    }

    public void StartGame()
    {
        Application.LoadLevel("GameMenuScene");
    }
}
