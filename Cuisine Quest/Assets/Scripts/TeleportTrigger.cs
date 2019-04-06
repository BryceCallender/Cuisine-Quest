using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour {
    public Vector2 EntranceDirection = new Vector2(0, -1);
    public Vector2 MyUnderworldCoerdinate;
    public Vector2 MyOverworldCoerdinate;

    public Vector2 PlayerIngressLocation;
    public Vector2 PlayerEgressLocation;



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
            float playerDirection = Vector3.Dot(EntranceDirection, collision.GetComponent<Rigidbody2D>().velocity);
            if (playerDirection < 0)
            {
                Vector3 cameraLocation = new Vector3(MyOverworldCoerdinate.x, MyOverworldCoerdinate.y, Camera.main.transform.position.z);
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().TeleportPlayer(cameraLocation, (Vector2)cameraLocation + PlayerEgressLocation);
            }
            else if (playerDirection > 0)
            {
                Vector3 cameraLocation = new Vector3(MyUnderworldCoerdinate.x, MyUnderworldCoerdinate.y, Camera.main.transform.position.z);
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().TeleportPlayer(cameraLocation, (Vector2)cameraLocation + PlayerIngressLocation);

            }

        }
    }
}
