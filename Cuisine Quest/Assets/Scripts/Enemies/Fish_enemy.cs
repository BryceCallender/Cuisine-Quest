﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_enemy : EnemyAbstract
{
    Transform target;
    Vector3 targetPos;
    Vector3 thisPos;
    public float offset;
    float angle;
    float minDistance = 3f;
    float range;
    bool playerFound = false;
    Rigidbody2D rb;
    float timer = 10f;

    // Use this for initialization
    void Start ()
    {
        health = 1;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            health -= 1;
        }
        if (health <= 0)
        {
            Die();
        }

        if (playerFound)
        {
            Move();
        }
        else if(timer > 1f)
        {
            FindPlayer();
        }
        else
        {
            timer += Time.deltaTime;
        }

    }
    void FindPlayer()
    {
        foreach(RaycastHit2D collide in Physics2D.CircleCastAll(transform.position, minDistance, new Vector2(0, 0)))
        {
            if(collide.collider.CompareTag("Player"))
            {
                target = collide.transform;
                print("Found");
                playerFound = true;
            }
        }
    }

    public override void Move()
    {
        targetPos = target.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        Vector2 movement = transform.up;
        movement = movement.normalized * speed;
        rb.velocity = movement;
    }

    public override void Attack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CiscoTesting>().health -= 1;
            rb.velocity = new Vector2(0, 0);
            playerFound = false;
            timer = 0.0f;

        }
    }

    void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}
