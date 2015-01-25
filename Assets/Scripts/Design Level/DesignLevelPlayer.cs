using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DesignLevelPlayer : MonoBehaviour
{
    // Player object
    [HideInInspector] public new Transform transform;
    [HideInInspector] public new Rigidbody2D rigidbody2D;
    public int WordCount;
    public float GoodCount;
    public float moveSpeed = 2f;

    void Awake()
    {
        transform	= GetComponent<Transform>();
        rigidbody2D	= GetComponent<Rigidbody2D>();
        
		//DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
		WordCount	= 0;
		GoodCount	= 0f;
        MainGame.GameTitle = "";
    }

	void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0f)
        {
            float right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x-1;
            Debug.Log("Move: right=" + right);
            if (transform.position.x < right)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else if (horizontal < 0f)
        {
            float left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x+1;
            Debug.Log("Move: left=" + left);
            if (transform.position.x > left)
            {
                transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    void LateUpdate()
    {
        if (WordCount == 3)
        {
            MainGame.DesignQuality = GoodCount / WordCount;
            Application.LoadLevel("GameMenuScene");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (WordCount < 3 && other.gameObject.CompareTag("DesignIdea"))
        {
            DesignIdea d = other.gameObject.GetComponent<DesignIdea>();

            if (d.Good) GoodCount += 1f;
            WordCount++;

            if (WordCount == 3)
            {
                MainGame.GameTitle += d.Word;
            }
            else
            {
                MainGame.GameTitle += d.Word + " ";
            }
            Destroy(d.gameObject);
        }
    }
}
