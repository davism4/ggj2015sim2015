using System;
using System.Collections.Generic;
using UnityEngine;

public class DesignIdeaGenerator : MonoBehaviour
{
    public float GoodWordChance = 0.25f;
    float leftLimit;
    float rightLimit;
    float yLimit;
    public GameObject DesignIdeaGameObject;
    float genTimer = 0f;
    float maxGenTimer = 1.5f;
    
    List<string> BadWords;
    List<string> GoodWords;

    void SetGoodWords()
    {
        GoodWords = new List<string>()
        {
            "Advance", "Action", "Adventure",
            "Baseball",
            "Candy", "Combat", "Craft",
            "Detective", "Dinosaur", "Dragon",
            "Eternal",
            "Heroes",
            "Knights",
            "Legend", "Lord",
            "Magic", "Monster", "Mystery",
            "Ninja",
            "Pirate", "Princess", "Puzzle",
            "Quantum",
            "Robot", "Roguelike", "RPG",
            "Samurai", "Space", "Super", "Sword",
            "Time Travel",
            "Warfare", "World",         
        };
    }

    void SetBadWords()
    {
        BadWords = new List<string>()
        {
            "Advertisement", "Agriculture",
            "Boredom", "Buggy",
            "Cleaner", "Cubicle",
            "Dentist", "Document", "Download",
            "Farming", "Finance",  "Fishing", "Fungal",
            "Glitchy",
            "Janitor",
            "Labor", "Layoffs", "Librarian", "Literature", "Low Budget",
            "Maintenance", "Measles",
            "Obscure", "Office",
            "Pencil", "Pigeon", "Politics", 
            "Reading", "Repetition",
            "Seminar", "Sludge", "Stationery", "Synergy",
            "Taxes",
            "Unstable", "Unresponsive",
            "Vegetation",
        };
        
    }
    
    void Awake() // once
    {
        leftLimit = transform.FindChild("Left Limit").transform.position.x;
        rightLimit = transform.FindChild("Right Limit").transform.position.x;
        yLimit = transform.FindChild("Right Limit").transform.position.y;
		DontDestroyOnLoad(transform.gameObject);
    }
    
    void Start() // each scene load
    {
		SetGoodWords();
		SetBadWords();
    }

	void Update()
    {
    	genTimer -= Time.deltaTime;
        if (genTimer <= 0)
        {
            if (maxGenTimer > 0.05f) maxGenTimer -= 0.0002f;
            genTimer = UnityEngine.Random.Range(maxGenTimer/2, maxGenTimer);
            CreateIdea();
        }
    }

    void CreateIdea()
    {
        string word;
        bool good;
        Vector3 spawnpoint = new Vector3(UnityEngine.Random.Range(leftLimit, rightLimit), yLimit, 0f);
        GameObject body = (GameObject)Instantiate(DesignIdeaGameObject, spawnpoint, Quaternion.identity);
        if (UnityEngine.Random.Range(0f, 1f) > GoodWordChance)
        {
            word = BadWords[UnityEngine.Random.Range(0, BadWords.Count-1)];
            good = false;
            BadWords.Remove(word);
            if (BadWords.Count == 0) SetBadWords();
        }
        else
        {
            word = GoodWords[UnityEngine.Random.Range(0, GoodWords.Count-1)];
            good = true;
            GoodWords.Remove(word);
            if (GoodWords.Count == 0) SetGoodWords();
        }
        body.GetComponent<DesignIdea>().SetWord (word, good);
    }

}
