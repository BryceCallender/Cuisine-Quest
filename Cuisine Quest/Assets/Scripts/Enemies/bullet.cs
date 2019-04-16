﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int speed;
    Vector3 velocity;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += velocity * speed * Time.deltaTime;
        
	}

    public void setVelocity(Vector2 vel)
    {
        velocity = vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Player"))
        {
            collision.GetComponent<CiscoTesting>().currentHealth -= 1;
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Area"))
        {
            Destroy(gameObject);
        }
    }
}
