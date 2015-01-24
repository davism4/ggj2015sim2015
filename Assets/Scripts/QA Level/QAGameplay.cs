using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QAGameplay : MonoBehaviour {

    public Object bug;
    public GameObject swatter;

    private List<Object> bugs;

	// Use this for initialization
	void Start () {
        bugs = new List<Object>();
        for (int i = 0; i < 5 /* what if we made this a function of code quality? */; i++)
            bugs.Add(GameObject.Instantiate(bug, new Vector3(Random.Range(-7, 7), Random.Range(-2, 4)), Quaternion.Euler(Vector3.back * Random.Range(-180, 180))));
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MainGame.QualityQuality += 0.05f;
            swatter.transform.position = ((GameObject)bugs[0]).transform.position;
            GameObject.Destroy(bugs[0]);
            bugs.RemoveAt(0);
            bugs.Add(GameObject.Instantiate(bug, new Vector3(Random.Range(-7, 7), Random.Range(-2, 4)), Quaternion.Euler(Vector3.back * Random.Range(-180, 180))));
        }
    }
}
