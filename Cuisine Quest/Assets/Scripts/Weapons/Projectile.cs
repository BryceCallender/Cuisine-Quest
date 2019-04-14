using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().CurrentArea.AddObj(gameObject);
        Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Area" && collision.tag != "Water")
        {
            Debug.Log(collision.name);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.parent = collision.transform;
        }
        
    }
}
