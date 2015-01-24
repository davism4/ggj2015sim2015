using System;
using System.Collections.Generic;
using UnityEngine;

public class DesignLevelPlayer : MonoBehaviour
{
    // Player object
    [HideInInspector] public new Transform transform;
    [HideInInspector] public new Rigidbody2D rigidbody2D;
    public int WordCount;
    public int GoodWordCount;
    public float moveSpeed = 2f;

    // Other
    MainGame mainGame;

    void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        mainGame = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGame>();
        WordCount = 0;
        GoodWordCount = 0;
    }

	void Update()
    {
        if (WordCount == 3)
        {
           MainGame.DesignQuality = ((float)GoodWordCount)/ WordCount;
           Application.LoadLevel("MainGameScene");
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (horizontal>0f)
            {
                rigidbody2D.velocity = Vector2.right * moveSpeed;
            }
            else if (horizontal<0f)
            {
                rigidbody2D.velocity = -Vector2.right * moveSpeed;
            }
            else
            {
            	rigidbody2D.velocity = Vector2.zero;
            }
        }
       
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (WordCount < 3 && other.gameObject.CompareTag("DesignIdea"))
        {
            DesignIdea d = other.gameObject.GetComponent<DesignIdea>();
            if (d.Good) GoodWordCount++;
            WordCount++;
            if (WordCount == 3)
            {
                MainGame.GameTitle += d.Word + " ";
            }
            else
            {
                MainGame.GameTitle += d.Word;
            }
        }
    }
}
