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

    public string title;

    void Awake()
    {
        transform	= GetComponent<Transform>();
        rigidbody2D	= GetComponent<Rigidbody2D>();
		DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
		WordCount	= 0;
		GoodCount	= 0f;
    }

	void Update()
    {
        title = MainGame.GameTitle;
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0f)
        {
            rigidbody2D.velocity = Vector2.right * moveSpeed;
        }
        else if (horizontal < 0f)
        {
            rigidbody2D.velocity = -Vector2.right * moveSpeed;
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
            MainGame.DesignQuality = ((float)GoodCount) / WordCount;            Application.LoadLevel("GameMenuScene");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (WordCount < 3 && other.gameObject.CompareTag("DesignIdea"))
        {
            DesignIdea d = other.gameObject.GetComponent<DesignIdea>();
            if (d.Good) GoodCount++;
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
