using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTrigger : MonoBehaviour {
    public Vector2 EntranceDirection = new Vector2(0, -1);
    public Vector2 MyDungeonCoordinate;
    public Vector2 MyOverworldCoordinate;

    public Vector2 PlayerIngressLocation;
    public Vector2 PlayerEgressLocation;


    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            float playerDirection = Vector3.Dot(EntranceDirection, collision.GetComponent<Rigidbody2D>().velocity);
            if(playerDirection < 0)
            {
                Vector3 cameraLocation = new Vector3(MyOverworldCoordinate.x, MyOverworldCoordinate.y, Camera.main.transform.position.z);
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().TeleportPlayer(cameraLocation, (Vector2)cameraLocation + PlayerEgressLocation);
            }else if(playerDirection > 0)
            {
                Vector3 cameraLocation = new Vector3(MyDungeonCoordinate.x, MyDungeonCoordinate.y, Camera.main.transform.position.z);
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().TeleportPlayer(cameraLocation, (Vector2)cameraLocation + PlayerIngressLocation);

            }
            
        }
    }
}
