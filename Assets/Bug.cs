using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour {

	[HideInInspector] new Transform transform;

    QAGameplay qa;
    float walktimer = 0f;
    public Vector3 forward;
    float speed;
    
    void Awake()
    {
        transform = GetComponent<Transform>();
        speed = UnityEngine.Random.Range(0.8f, 2f);
        forward = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0);
        qa = GameObject.FindObjectOfType<QAGameplay>();
    }

    void Update()
    {
        if (walktimer > 0f)
        {
            walktimer -= Time.deltaTime;
            transform.position += forward * Time.deltaTime * speed;
        }
        else
        {
            walktimer = UnityEngine.Random.Range(0.15f, 0.8f);
            
            transform.Rotate(Vector3.forward, 4f*UnityEngine.Random.Range(-5f, 5f));
            forward = transform.rotation * Vector3.right;
            speed = UnityEngine.Random.Range(0.8f, 2f);
        }
        if (Mathf.Abs(transform.position.x) > 8f || Mathf.Abs(transform.position.y) > 7f)
        {
            qa.MakeNewBug(this.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }
}
