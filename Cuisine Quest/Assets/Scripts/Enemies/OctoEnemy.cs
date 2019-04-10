﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoEnemy : EnemyAbstract
{
    int travel_path, paths_traveled = 0;
    float timer;
    public GameObject bullet;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (timer > 0.3f && paths_traveled < 4)
        {
            travel_path = Random.Range(1, 5);
            if (travel_path == 1)
            {
                transform.Rotate(0, 0, 90);
            }
            else if (travel_path == 2)
            {
                transform.Rotate(0, 0, -90);
            }
            else if (travel_path == 3)
            {
                transform.Rotate(0, 0, 180);
            }
            timer = 0.0f;
            paths_traveled += 1;
        }
        else if (paths_traveled < 4)
        {
            Vector2 movement = transform.up;
            movement = movement.normalized * speed;
            rb.velocity = movement;
            
            timer += Time.deltaTime;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            if (timer > 0.5f)
            {
                GameObject bulletinstance;
                bulletinstance = Instantiate(bullet, transform.position, transform.rotation);
                bulletinstance.GetComponent<bullet>().setVelocity(transform.right);
                timer = 0.0f;
                paths_traveled = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }

        }
    }
}
