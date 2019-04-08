using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour {
    public GameObject Player;
    public Transform Camera;

    public Area[] Areas;
    public AreaScriptable[] AreaTest;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TeleportPlayer(Vector3 cameraLocation, Vector2 playerLocation)
    {
        Camera.position = cameraLocation;
        Player.transform.position = (Vector2)cameraLocation + playerLocation;
    }

    public void TeleportPlayer(string location, Vector2 playerLocation){
        AreaScriptable locationArea = getAreaScriptable(location);
        if (locationArea != null)
        {
            TeleportPlayer((Vector3)locationArea.Location + new Vector3(0, 0, Camera.transform.position.z), playerLocation);
        }
        else
        {
            Debug.Log("Area not found");
        }
    }

    public void TeleportPlayer(string location){
        AreaScriptable locationArea = getAreaScriptable(location);
        if(locationArea != null){
            TeleportPlayer((Vector3)locationArea.Location + new Vector3(0,0,Camera.transform.position.z), locationArea.DefaultPlayerLocation);
        }else{
            Debug.Log("Area not found");
        }
    }


    private Area getArea(string location){
        Area locationArea = null;
        foreach(Area a in Areas){
            if (a.Name == location) locationArea = a;
        }

        return locationArea;
    }


    private AreaScriptable getAreaScriptable(string location){
        AreaScriptable locationArea = null;
        foreach(AreaScriptable a in AreaTest){
            if (a.name == location) locationArea = a;
        }


        return locationArea;
    }
}

[System.Serializable]
public class Area
{
    public Vector2 Location;
    public string Name;
    public Vector2 DefaultPlayerLocation;
}
