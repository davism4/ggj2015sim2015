using UnityEngine;
using System.Collections;

public class Swatter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > -11)
            transform.Translate(0, -1, 0);
	}
}
