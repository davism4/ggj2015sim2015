using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static GameObject manager;
    Transform imageTransform;
    Text text;

    void Awake()
    {
        text = GetComponentInChildren<Text>();
        imageTransform = transform.GetChild(0);
    }

    void Start()
    {
        if (manager == null)
        {
            manager = this.gameObject;
        }
        
        if (this.gameObject != manager)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }


    float sizeScale = 1;
    bool expanding = true;
    void Update()
    {
        float time = MainGame.time;
        float panicScale = 1 - time / 48;
        text.text = time > 0f ? time.ToString("##.00") : "0.00";
        text.color = new Color(panicScale, 0, 0);

        if (expanding)
        {
            if (sizeScale < 1.2f)
                sizeScale += (1+panicScale) * Time.deltaTime * 0.25f;
            else
                expanding = false;
        }
        else
        {
            if (sizeScale > 1)
                sizeScale -= (1+panicScale) * Time.deltaTime * 0.25f;
            else
                expanding = true;
        }

        //scale = 1 + 0.2f*Mathf.Abs(Mathf.Sin(10*panicScale*Mathf.PI));
        imageTransform.localScale = new Vector3(sizeScale, sizeScale, 1);

        //text.fontSize = Mathf.RoundToInt(text.fontSize*(1+panicScale*0.25f));
        //float offsetx = 0;//panicScale * Mathf.Cos(time+(5*panicScale) ) * 0.5f; // shaking?
        //float offsety = 0;//panicScale * Mathf.Sin(time+(4*panicScale) ) * 1;
        //timerStyle = GUI.skin.GetStyle("Label");
        //timerStyle.alignment = TextAnchor.MiddleLeft;
        // draw
        //GUI.Label(new Rect(5 + offsetx, 5 + offsety, 110, 110), timerTexture);
        if (time <= 0 && Application.loadedLevelName != "MainGameScene")
        {
            //Debug.Log("TIME'S UP!");
            Application.LoadLevel("GameMenuScene");
        }

    }

}
