using System;
using System.Collections.Generic;
using UnityEngine;

public class DesignIdeaGenerator : MonoBehaviour
{
    float GoodWordChance = 0.48f;
    
    float genTimer = 0f;
    float maxGenTimer = 1.5f;

    Vector3[] spawnpoints;
    int NumberSpawnPoints = 7;
    int lastSpawnpointIndex;

    public GameObject GoodDesignIdea;
    public GameObject BadDesignIdea;
    
    List<string> BadWords;
    List<string> GoodWords;

    void SetGoodWords()
    {
        GoodWords = new List<string>()
        {
            "Advance", "Action", "Adventure", "Apocalypse",
            "Baseball", "Battleaxe",
            "Candy", "Combat", "Craft",
            "Detective", "Dinosaur", "Dragon", "Dungeon",
            "Eternity",
            "Fantasy", "Fiction",
            "Hero",
            "Knight",
            "Legend", "Lord",
            "Magic", "Monster", "Mystery",
            "Ninja",
            "Online",
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
            "Big Rigs", "Boredom", "Buffering", "Bureaucrat",
            "Cleaner", "Comatose", "Conformity", "Cubicle",
            "Dating", "Dentist", "Document", "Dust",
            "Fedora", "Feelings", "Finance",  "Fishing", "Fork", "Freemium",
            "Generic",
            "Hipster",
            "Infection", "Intolerance",
            "Labor", "Lag", "Lame", "Layoffs", "Library", "Literacy", "LOLcat",
            "Maintenance", "Measles",
            "Nap",
            "Obscure", "Office",
            "Pay-to-Win", "Pencil", "Pigeon", "Politics",
            "Racist", "Reading", "Repetition",
            "Seminar", "Sexist", "Simulator", "Slow", "Sonic", "Subscription", "Synergy",
            "Taxes", "Tiger Woods", "Tutorial",
            "Unstable",
            "Vague",
        };
        
    }

    void Awake()
    {
        float left = 0.9f*Camera.main.ScreenToWorldPoint(new Vector3(0, 0 ,0)).x;
        float interval = (-2* left) / (NumberSpawnPoints - 1);
        spawnpoints = new Vector3[NumberSpawnPoints];
        for (int i = 0; i < NumberSpawnPoints; i++)
        {
            spawnpoints[i] = new Vector3(left + i * interval, transform.position.y, 0);
        }
        lastSpawnpointIndex = UnityEngine.Random.Range(0, spawnpoints.Length - 1);
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
        GameObject body;
        Vector3 spawnpoint = spawnpoints[lastSpawnpointIndex];

        int npawn = lastSpawnpointIndex;
        while (npawn == lastSpawnpointIndex)
        {
            npawn = UnityEngine.Random.Range(0, NumberSpawnPoints - 1);
        }
        lastSpawnpointIndex = npawn;

        if (UnityEngine.Random.Range(0f, 1f) <= GoodWordChance)
        {
            body = (GameObject)Instantiate(GoodDesignIdea, spawnpoints[npawn], Quaternion.identity);
            word = GoodWords[UnityEngine.Random.Range(0, GoodWords.Count-1)];
            GoodWords.Remove(word);
            if (GoodWords.Count == 0) SetGoodWords();
            GoodWordChance = 0.46f; // good => bad more likely
        }
        else
        {
            body = (GameObject)Instantiate(BadDesignIdea, spawnpoint, Quaternion.identity);
            word = BadWords[UnityEngine.Random.Range(0, BadWords.Count - 1)];
            BadWords.Remove(word);
            if (BadWords.Count == 0) SetBadWords();
            GoodWordChance = 0.54f; // bad => good more likely
        }
        
        body.transform.GetComponent<DesignIdea>().Word = word;
    }

}
