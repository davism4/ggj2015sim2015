using System;
using System.Collections.Generic;
using UnityEngine;

public class DesignIdeaGenerator : MonoBehaviour
{
    float GoodWordChance = 0.25f;
    
    float genTimer = 0f;
    float maxGenTimer = 1.5f;

    public GameObject GoodDesignIdea;
    public GameObject BadDesignIdea;
    
    List<string> BadWords;
    List<string> GoodWords;

    void SetGoodWords()
    {
        GoodWords = new List<string>()
        {
            "Advance", "Action", "Adventure",
            "Baseball", "Battleaxe",
            "Candy", "Combat", "Craft",
            "Detective", "Dinosaur", "Dragon", "Dungeon",
            "Eternal",
            "Fantasy", "Fiction",
            "Hero",
            "Knight",
            "Legend", "Lord",
            "Magic", "Monster", "Mystery",
            "Ninja",
            "Pirate", "Princess", "Puzzle",
            "Quantum", "Quest",
            "Robot", "RPG",
            "Samurai", "Sci-Fi", "Space", "Super", "Sword",
            "Time Travel",
            "Warfare", "World",         
        };
    }

    void SetBadWords()
    {
        BadWords = new List<string>()
        {
            "Advertisement", "Agriculture",
            "Big Rigs", "Boredom", "Buggy", "Bureaucrat",
            "Cleaner", "Comatose", "Cubicle",
            "Dating", "Dentist", "Document", "Download",
            "Farming", "Feelings", "Finance",  "Fishing", "Fork", "Freemium",
            "Glitch",
            "Janitor",
            "Labor", "Layoffs", "Librarian", "Literature", "Lolcat",
            "Maintenance", "Measles",
            "Obscure", "Office",
            "Pay-to-Win", "Pencil", "Pigeon", "Politics", 
            "Reading", "Repetition",
            "Seminar", "Simulator", "Sludge", "Sonic", "Subscription", "Synergy",
            "Taxes", "Tiger Woods",
            "Unstable", "Unresponsive",
            "Vegetation",
        };
        
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
        float left = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        float right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x;
        float y = transform.position.y;// Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        Debug.Log("Create idea: right=" + right + ", left = " + left + ", y = " + y);

        Vector3 spawnpoint = new Vector3(UnityEngine.Random.Range(left, right), y, 0f);
        GameObject body;
        if (UnityEngine.Random.Range(0f, 1f) <= GoodWordChance)
        {
            body = (GameObject)Instantiate(GoodDesignIdea, spawnpoint, Quaternion.identity);
            word = GoodWords[UnityEngine.Random.Range(0, GoodWords.Count-1)];
            GoodWords.Remove(word);
            if (GoodWords.Count == 0) SetGoodWords();
            GoodWordChance -= 0.015f; // change likelihood
        }
        else
        {
            body = (GameObject)Instantiate(BadDesignIdea, spawnpoint, Quaternion.identity);
            word = BadWords[UnityEngine.Random.Range(0, BadWords.Count - 1)];
            BadWords.Remove(word);
            if (BadWords.Count == 0) SetBadWords();
            GoodWordChance += 0.015f; // change likelihood
        }
        
        body.transform.GetComponent<DesignIdea>().Word = word;
    }

}
