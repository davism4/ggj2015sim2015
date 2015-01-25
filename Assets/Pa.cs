using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pa : MonoBehaviour {

    new RectTransform rectTransform;

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Demo_Character.Finished && rectTransform.position.y < 500)
            rectTransform.Translate(0, 200 * Time.deltaTime, 0);
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
