using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButtonText : MonoBehaviour {

    public Text ArtText;
    public Text CodeText;
    public Text DesignText;
    public Text SoundText;

    void Start()
    {
        Debug.Log("code quality" + MainGame.CodeQuality);
        ArtText.text = "Art!\n("       + Mathf.RoundToInt(MainGame.ArtQuality*100)    + "%)";
        CodeText.text = "Coding!\n("   + Mathf.RoundToInt(MainGame.CodeQuality*100)   + "%)";
        DesignText.text = "Design!\n(" + Mathf.RoundToInt(MainGame.DesignQuality*100) + "%)";
        SoundText.text = "Music!\n("   + Mathf.RoundToInt(MainGame.AudioQuality*100)  + "%)";
    }
}
