using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour {
    
    public string TeleportLocation;
    public Vector2 PlayerLocation;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(TeleportLocation != ""){
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().TeleportPlayer(TeleportLocation, PlayerLocation);
            }else{
                Debug.Log("TeleportLocation missing!");
            }



        }
    }
}
