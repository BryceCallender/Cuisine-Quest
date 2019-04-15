using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public PlayerController Player;
    public Hearts Health;

    public Vector2 transitionGrid = new Vector2(16, 10);
    public float TransitionSpeed = 10f;
    private bool transitioning = false;
    private Vector3 transitionDestination;
    public float TransitionBuffer = 0.1f;

    public bool DungeonExit = false;

    //public BoxCollider2D[] MyColliders;
	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update () {
        if (DungeonExit)
        {
            transitioning = false;
            Player.playerCanMove = true;
        }
        if (transitioning)
        {
            Vector3 newPosition =  Vector3.MoveTowards(transform.position, transitionDestination, TransitionSpeed * Time.deltaTime);
            transform.position = newPosition;

            if(Vector3.Distance(transitionDestination, transform.position) < TransitionBuffer)
            {
                transform.position = transitionDestination;
                transitioning = false;
                Player.playerCanMove = true;
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().FinishAreaMove();
            }
        }
	}

    public void SetDungeonExit(bool state) {
        DungeonExit = state;
        
    }

    public bool GetTransitioning()
    {
        return transitioning;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" && !transitioning)
        {
            handlePlayerTracking();
        }
    }

    float edgeBuffer = 0.7f;
    private void handlePlayerTracking()
    {
        //moving left
        if(Player.transform.position.x <= transform.position.x - transitionGrid.x /2 + edgeBuffer)
        {
            SceneTransition(new Vector2(-1, 0));
        }
        //moving right
        else if (Player.transform.position.x >= transform.position.x + transitionGrid.x / 2 - edgeBuffer)
        {
            SceneTransition(new Vector2(1, 0));
        }
        //moving up
        else if (Player.transform.position.y >= transform.position.y + transitionGrid.y / 2 - edgeBuffer)
        {
            SceneTransition(new Vector2(0, 1));
        }
        //moving down
        else if (Player.transform.position.y <= transform.position.y - transitionGrid.y / 2 + edgeBuffer)
        {
            SceneTransition(new Vector2(0, -1));
        }
    }

    public void SceneTransition(Vector2 direction)
    {
        if (true && !transitioning) //check level handler for transition check
        {
            transitioning = true;
            transitionDestination = direction * transitionGrid ;
            transitionDestination += transform.position;
            Debug.Log(transitionDestination.ToString());

            Player.playerCanMove = false;

        }
    }
}
