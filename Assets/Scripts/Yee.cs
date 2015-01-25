using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Yee : MonoBehaviour {

    Text text;
    float red;
    bool rising;

    void Awake()
    {
        text = GetComponent<Text>();
        red = 0;
        rising = true;
    }

    void Update () {
        if (MainGame.goingToResults)
            text.text = "Time's Up!";
        else
            text.text = "What do we do now?";

        float panicScale = 1 - MainGame.time / 48;
        
        if (rising)
        {
            if (red < panicScale) red += Time.deltaTime * (5*panicScale);
            else rising = false;
        }
        else
        {
            if (red > 0) red -= Time.deltaTime * (5*panicScale);
            else rising = true;
        }

        text.color = new Color(red, 0, 0);
        //yee
	}
}
