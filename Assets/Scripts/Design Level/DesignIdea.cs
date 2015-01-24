using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class DesignIdea : MonoBehaviour {

    public string Word;
    public bool Good = true;
    [HideInInspector] public new Transform transform;
    float speed;
    public float SpeedMin = 1.0f;
	public float SpeedMax = 1.5f;
    public GUIStyle guiStyle;
	
	void Awake()
    {
    	transform = GetComponent<Transform>();
       //guiStyle = GetComponent<GUIStyle>();
        speed = UnityEngine.Random.Range(SpeedMin, SpeedMax);
        //rigidbody2D.velocity = -Vector2.up * UnityEngine.Random.Range(SpeedMin, SpeedMax);
        //text = GetComponent<Text>();
	}
	
	public void SetWord(string w, bool good)
	{
		Word = w;
		//text.text = Word;
        if (good) renderer.material.color = Color.green;
        else renderer.material.color = Color.red;
	}
	
	void Update ()
    {
        transform.position -= Time.deltaTime * speed * Vector3.up;
       // text.transform.position = this.transform.position;
		if (transform.position.y < -20f)
		{
			Destroy(this.gameObject);
		}
	}

    void OnGUI()
    {
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        Rect r = new Rect(
            point.x - Word.Length,//*guiText.fontSize/2,
            Screen.height-point.y,//-guiText.fontSize,
            Word.Length,// * 2 * guiText.fontSize,
            2//*guiText.fontSize
        );
        GUI.Label(r, Word, guiStyle);
    }
}
