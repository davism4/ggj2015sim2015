using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class QAGameplay : MonoBehaviour {

    public Text text;
    public Object bug;
    public GameObject swatter;

    private List<Object> bugs;

    int bugsSquashed;

	// Use this for initialization
	void Start () {
        bugsSquashed = 0;
        bugs = new List<Object>();
        for (int i = 0; i < 5 /* what if we made this a function of code quality? */; i++)
            bugs.Add(GameObject.Instantiate(bug, new Vector3(Random.Range(-7, 7), Random.Range(-2, 4)), Quaternion.Euler(Vector3.back * Random.Range(-180, 180))));
	}

    public void MakeNewBug(GameObject b)
    {
        bugs.Remove(b);
        GameObject.Destroy(b);
        bugs.Add(GameObject.Instantiate(bug, new Vector3(Random.Range(-7, 7), Random.Range(-2, 4)), Quaternion.Euler(Vector3.back * Random.Range(-180, 180))));
    }

    void OnDisable()
    {
        Debug.Log("CLOSING QA");
        float increase = 0.002f;
        for (int i = 0; i < bugsSquashed; i++)
        {
            float[] qualities = {MainGame.ArtQuality, MainGame.AudioQuality, MainGame.CodeQuality, MainGame.DesignQuality};
            if (MainGame.ArtQuality <= qualities.Min())
            {
                MainGame.ArtQuality += increase;
            }
            else if (MainGame.AudioQuality <= qualities.Min())
            {
                MainGame.AudioQuality += increase;
            }
            else if (MainGame.CodeQuality <= qualities.Min())
            {
                MainGame.CodeQuality += increase;
            }
            else
            {
                MainGame.DesignQuality += increase;
            }
        }
    }

	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //MainGame.QualityQuality += 0.05f;
            bugsSquashed++;
            text.text = "Problems solved: " + bugsSquashed;

            swatter.transform.position = ((GameObject)bugs[0]).transform.position;

            GameObject.Destroy(bugs[0]);
            bugs.RemoveAt(0);
            bugs.Add(GameObject.Instantiate(bug, new Vector3(Random.Range(-7, 7), Random.Range(-2, 4)), Quaternion.Euler(Vector3.back * Random.Range(-180, 180))));
        }
    }
}
