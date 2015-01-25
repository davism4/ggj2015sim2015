using UnityEngine;
using System.Collections;

public class Swatter : MonoBehaviour {

    [HideInInspector] public new AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
        if (transform.position.y > -11)
            transform.Translate(0, -1, 0);
	}
}
