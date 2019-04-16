using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CiscoTesting : MonoBehaviour 
{
    public float WalkingSpeed = 10f;
    public bool HasMovementControl = true;

    public Weapon CurrentWeapon;
    public Weapon[] Weapons;
    public Vector2 DirectionFacing = new Vector2(0, -1);

    public int currentHealth;
    public int maxHealth = 5;

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
	void Update () {

        if (HasMovementControl) handlMovement();

        bool primaryAttack = Input.GetMouseButtonDown(0);
        if (primaryAttack && CurrentWeapon != null)
        {
            CurrentWeapon.Attack(DirectionFacing);
        }
        if (Input.GetMouseButton(0)) primaryAttack = true;
        if (Input.GetMouseButtonDown(1) && CurrentWeapon != null)
        {
            CurrentWeapon.AttackSecondary(DirectionFacing, primaryAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentWeapon = Weapons[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentWeapon = Weapons[1];
        }

        if (currentHealth <= 0)
        {
            Die();
        }
	
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentWeapon = Weapons[2];
        }
    }

    void Die()
    {
        SceneManager.LoadScene(0);
    }
    private void handlMovement()
    {
        float vMove = 0; // = Input.GetAxis("Vertical");
        float hMove = 0; // = Input.GetAxis("Horizontal");

        Vector2 newFacingDirection = DirectionFacing;

        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) || (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)))
        {
            vMove = 0;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vMove = 1;
            newFacingDirection = new Vector2(0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vMove = -1;
            newFacingDirection = new Vector2(0, -1);
        }

        if ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) || (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)))
        {
            hMove = 0;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            hMove = 1;
            newFacingDirection = new Vector2(1, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            hMove = -1;
            newFacingDirection = new Vector2(-1, 0);
        }

        //checking if Player movement direction has changed and if 
        //current weapon will allow character sprite direction change
        if(newFacingDirection != DirectionFacing)
        {
            if (CurrentWeapon.AttackAbort())
            {
                DirectionFacing = newFacingDirection;
            }
        }
        //Debug.Log(DirectionFacing.ToString());

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
