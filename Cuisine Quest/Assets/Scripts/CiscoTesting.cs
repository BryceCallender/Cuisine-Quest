using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CiscoTesting : MonoBehaviour 
{
    public float WalkingSpeed = 10f;

    //public int currentHealth;
    //public int maxHealth = 5;
    public HealthSystem health = new HealthSystem();

    private PlayerQuestSystem playerQuestSystem;
    public Dictionary<string, int> items;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start ()
    {
        items = new Dictionary<string, int>();
        rb = GetComponent<Rigidbody2D>();
        health.setMaxHealth(5);
        health.ResetHealth();
        currentHealth = maxHealth;
        playerQuestSystem = GetComponent<PlayerQuestSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (HasMovementControl) handlMovement();

        if (health.isAlive() == false)
        {
            Die();
        }
	}

    void Die()
    {
        SceneManager.LoadScene(0);
    }

    public void AddItem(GameObject item)
    {
        string itemName = item.name;
        if(items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            Debug.Log("Adding " + item.name);
            items.Add(itemName, 1);
        }

        //Check for completion of the quest when an item is picked up
        foreach (Quest quest in playerQuestSystem.GetQuests())
        {
            if (quest.questData.questState == QuestState.inProgress)
            {
                quest.CheckCompletion(this);
            }
        }
    }

}
