using UnityEngine;
using System.Collections;

public class PenFollowCursor : MonoBehaviour {

    new Transform transform;

    void Awake()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v.z = 0;
        transform.position = v;
    }

}
