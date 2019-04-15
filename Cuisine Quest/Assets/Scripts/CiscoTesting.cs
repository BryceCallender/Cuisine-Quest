using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiscoTesting : MonoBehaviour {
    public float WalkingSpeed = 10f;
    public bool HasMovementControl = true;

    public Weapon CurrentWeapon;
    public Weapon[] Weapons;
    public Vector2 DirectionFacing = new Vector2(0, -1);

    public int FishMeat = 0;
    public int Greens = 0;
    public int Lemons = 0;

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
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentWeapon = Weapons[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentWeapon = Weapons[2];
        }
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
