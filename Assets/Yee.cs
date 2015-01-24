using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Yee : MonoBehaviour {

	// Use this for initialization
	void Update () {
        if (MainGame.goingToResults)
            GetComponent<Text>().text = "Time's Up!";
        else
            GetComponent<Text>().text = "What do we do now?";
	}
}
