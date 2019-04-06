using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiscoTesting : MonoBehaviour {
    public float WalkingSpeed = 10f;
    public bool HasMovementControl = true;
    

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (HasMovementControl) handlMovement();
        
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
}
