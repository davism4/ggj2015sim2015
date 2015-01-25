using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Results : MonoBehaviour {

    public GameObject DesignText;
    public GameObject ArtText;
    public GameObject CodeText;
    public GameObject SoundText;
    public GameObject QAText;
    public GameObject GGButton;
    public GameObject TitleText;

    private Text design, art, code, sound, qa, title;

    private float delay;

	// Use this for initialization
    void Start()
    {
        design = DesignText.GetComponent<Text>();
        art = ArtText.GetComponent<Text>();
        code = CodeText.GetComponent<Text>();
        sound = SoundText.GetComponent<Text>();
        qa = QAText.GetComponent<Text>();
        title = TitleText.GetComponent<Text>();
        delay = 0;
	}
	
	// Update is called once per frame
	void Update () {
        delay += Time.deltaTime;
        if (delay >= 1f && design.text.Length < 9)
            design.text += (MainGame.DesignQuality * 100) + "%";
        if (delay >= 1.5f && art.text.Length < 6)
            art.text += (MainGame.ArtQuality * 100) + "%";
        if (delay >= 2f && code.text.Length < 7)
            code.text += (MainGame.CodeQuality * 100) + "%";
        if (delay >= 2.5f && sound.text.Length < 8)
            sound.text += (MainGame.AudioQuality * 100) + "%";
        if (delay >= 3f && qa.text.Length < 5)
            qa.text += (MainGame.QualityQuality * 100) + "%";
        if (delay >= 4f)
            GGButton.SetActive(true);
	}

    public void StartOver()
    {
        MainGame.CodeQuality = 0;
        MainGame.ArtQuality = 0;
        MainGame.AudioQuality = 0;
        MainGame.DesignQuality = 0;
        MainGame.QualityQuality = 0;
        MainGame.goingToResults = false;
        MainGame.resultsTime = 0;
        MainGame.MusicSource.time = 0;
        MainGame.time = 48f;
        Destroy(MainGame.manager.gameObject);
        MainGame.manager = null;
        Application.LoadLevel("StartMenuScene");
    }
}
