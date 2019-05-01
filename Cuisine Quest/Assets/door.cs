using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{

    public Item key;
    BoxCollider2D gate;

    // Use this for initialization
    void Start ()
    {
        gate = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.gameObject.GetComponent<CiscoTesting>().items[key] > 0)
        {
            gate.enabled = false;
        }
    }
}
