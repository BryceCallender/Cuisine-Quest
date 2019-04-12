using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiscoTesting : MonoBehaviour {
    public float WalkingSpeed = 10f;
    public bool HasMovementControl = true;

    public int FishMeat = 0;
    public int Greens = 0;
    public int Lemons = 0;
    public int health = 1;

    public Quest[] MyQuest;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        foreach(Quest q in MyQuest)
        {
            q.State = Quest.QuestState.inProgress;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (HasMovementControl) handlMovement();
        if(health <= 0)
        {
            print("dead");
        }

        
        
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

    public void AddItem(string type, int count)
    {
        switch (type)
        {
            case "LemonJuice":
                Lemons += count;
                break;
            case "Greens":
                Greens += count;
                break;
            case "FishMeat":
                FishMeat += count;
                break;
            default:
                Debug.Log("Item not found.");
                break;
        }

        foreach (Quest q in MyQuest)
        {
            if(q.State != Quest.QuestState.completed) q.CheckCompletion(this);
        }
    }

}
