using System;
using System.Collections.Generic;
using UnityEngine;

public class DesignIdeaGenerator : MonoBehaviour
{
    public float GoodWordChance = 0.45f;
    float leftLimit;
    float rightLimit;
    float yLimit;
    public GameObject DesignIdeaGameObject;
    float genTimer = 0f;
    float maxGenTimer = 1.5f;
    
    List<string> BadWords;
    List<string> GoodWords;

    void SetLists()
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

    void Awake()
    {
        SetLists();
        leftLimit = transform.FindChild("Left Limit").transform.position.x;
        rightLimit = transform.FindChild("Right Limit").transform.position.x;
        yLimit = transform.FindChild("Right Limit").transform.position.y;
    }

	void Update()
    {
    	genTimer -= Time.deltaTime;
        if (genTimer <= 0)
        {
            genTimer = UnityEngine.Random.Range(maxGenTimer/2, maxGenTimer);
            CreateIdea();
        }
    }

    void CreateIdea()
    {
        string word;
        Vector3 spawnpoint = new Vector3(UnityEngine.Random.Range(leftLimit, rightLimit), yLimit, 0f);
        GameObject body = (GameObject)Instantiate(DesignIdeaGameObject, spawnpoint, Quaternion.identity);
        if (UnityEngine.Random.Range(0f, 1f) > GoodWordChance)
        {
            word = BadWords[UnityEngine.Random.Range(0, BadWords.Count)];
        }
        else
        {
            word = GoodWords[UnityEngine.Random.Range(0, GoodWords.Count)];
        }
        body.GetComponent<DesignIdea>().SetWord (word);
    }

}
