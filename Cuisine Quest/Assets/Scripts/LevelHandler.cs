using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour {
    public GameObject Player;
    public Transform Camera;

    public Area[] Areas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TeleportPlayer(Vector3 cameraLocation, Vector2 playerLocation)
    {
        Camera.position = cameraLocation;
        Player.transform.position = playerLocation;
    }
}

[System.Serializable]
public class Area
{
    public Vector2 Location;
    public string Name;
    public Vector2 DefaultPlayerLocation;
}
