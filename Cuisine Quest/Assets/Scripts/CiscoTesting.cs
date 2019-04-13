using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CiscoTesting : MonoBehaviour 
{
    public float WalkingSpeed = 10f;
    public bool HasMovementControl = true;

<<<<<<< HEAD
=======
    public int FishMeat = 0;
    public int Greens = 0;
    public int Lemons = 0;

    public int currentHealth;
    public int maxHealth = 5;


>>>>>>> 03f653cf725a770921f1fdb96cb09d07290f620d
    public Quest[] MyQuest;
    public Dictionary<GameObject, int> items;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start ()
    {
        items = new Dictionary<GameObject, int>();
        rb = GetComponent<Rigidbody2D>();
        foreach(Quest q in MyQuest)
        {
            q.questData.questState = QuestState.inProgress;
        }
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (HasMovementControl) handlMovement();
<<<<<<< HEAD
=======
        if (currentHealth <= 0)
        {
            Die();
        }
>>>>>>> 03f653cf725a770921f1fdb96cb09d07290f620d
	}

    void Die()
    {
        SceneManager.LoadScene(0);
    }
    private void handlMovement()
    {
        float vMove = 0; // = Input.GetAxis("Vertical");
        float hMove = 0; // = Input.GetAxis("Horizontal");

        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) || (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)))
        {
            vMove = 0;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vMove = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vMove = -1;
        }

        if ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) || (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)))
        {
            hMove = 0;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            hMove = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            hMove = -1;
        }


        Vector2 movement = new Vector2(hMove, vMove);
        movement = movement.normalized * WalkingSpeed;
        rb.velocity = movement;
    }

    public void AddItem(GameObject item)
    {
        if(items.ContainsKey(item))
        {
            items[item]++;
        }
        else
        {
            items.Add(item,0);
        }

        //Check for completion of the quest when an item is picked up
        foreach (Quest quest in MyQuest)
        {
            if (quest.questData.questState != QuestState.completed)
            {
                quest.CheckCompletion(this);
            }
        }
    }
}
