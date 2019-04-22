using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTrigger : MonoBehaviour {
    public Vector2 EntranceDirection = new Vector2(0, -1);
    public Vector2 MyDungeonCoordinate;
    public Vector2 MyOverworldCoordinate;

    public AreaScriptable IngressLocation;
    public AreaScriptable EgressLocation;

    public Vector2 PlayerIngressLocation;
    public Vector2 PlayerEgressLocation;

    public AreaAbstract[] DungeonAreas;
    public AreaAbstract[] OverWorld;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            float playerDirection = Vector3.Dot(EntranceDirection, collision.GetComponent<Rigidbody2D>().velocity);
            if(playerDirection < 0)
            {
                //Vector3 cameraLocation = new Vector3(MyOverworldCoordinate.x, MyOverworldCoordinate.y, Camera.main.transform.position.z);
                SetAreaColliders(false, true);
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().TeleportPlayer(EgressLocation.name, PlayerEgressLocation);
            }else if(playerDirection > 0)
            {
                //Vector3 cameraLocation = new Vector3(MyDungeonCoordinate.x, MyDungeonCoordinate.y, Camera.main.transform.position.z);
                SetAreaColliders(true, false);
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().TeleportPlayer(IngressLocation.name,  PlayerIngressLocation);
            }
        }
    }

    private void SetAreaColliders(bool dungeon, bool overworld)
    {
        foreach(AreaAbstract d in DungeonAreas)
        {
            d.GetComponent<BoxCollider2D>().enabled = dungeon;
        }

        foreach (AreaAbstract o in OverWorld)
        {
            o.GetComponent<BoxCollider2D>().enabled = overworld;
        }
    }
}
