using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DesignIdea : MonoBehaviour {

    public string Word;
    public bool Good = true;
    [HideInInspector] public new Transform transform;
    public float SpeedMin = 1.0f;
	public float SpeedMax = 1.5f;
	Text text;
	
	void Awake()
    {
    	transform = GetComponent<Transform>();
        rigidbody2D.velocity = -Vector2.up * UnityEngine.Random.Range(SpeedMin, SpeedMax);
        text = GetComponent<Text>();
	}
	
	public void SetWord(string w)
	{
		Word = w;
		text.text = Word;
	}
	
	void Update ()
    {
        if (transform.position.y < -20f)
        {
        	Destroy(this.gameObject);
        }
        text.transform.position = this.transform.position;
	}
	
}
