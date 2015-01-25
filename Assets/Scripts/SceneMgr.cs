using UnityEngine;
using System.Collections;

public class SceneMgr : MonoBehaviour {

    public void StartCode()
    {
        if (MainGame.time > 0)
        Application.LoadLevel("CodeScene");
    }

    public void StartArt()
    {
        if (MainGame.time > 0)
        Application.LoadLevel("ArtScene");
    }

    public void StartSound()
    {
        if (MainGame.time > 0)
        Application.LoadLevel("MusicScene");
    }

    public void StartDesign()
    {
        if (MainGame.time > 0)
        Application.LoadLevel("DesignScene");
    }

    public void StartGame()
    {
        Application.LoadLevel("GameMenuScene");
    }
}
