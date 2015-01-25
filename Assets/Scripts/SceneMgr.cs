using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneMgr : MonoBehaviour {

    public GameObject QAbutton;
    float minimumPercentage = 0.35f;

    void Awake()
    {
        if (QAbutton != null)
            QAbutton.SetActive(false);
    }

    void Update()
    {
        if (QAbutton != null && !QAbutton.activeSelf)
        {
            if (MainGame.time > 0f &&
                MainGame.CodeQuality > minimumPercentage &&
                MainGame.AudioQuality > minimumPercentage &&
                MainGame.DesignQuality > minimumPercentage &&
                MainGame.ArtQuality > minimumPercentage)
            {
                QAbutton.SetActive(true);
            }
        }
    }

    public void StartQA()
    {
        if (MainGame.time > 0)
            Application.LoadLevel("QAScene");
    }

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
