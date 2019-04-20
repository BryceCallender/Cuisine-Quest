using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyAbstract {

    public Transform player;
    float timer;
    int travel_path;
    Rigidbody2D rb;


	// Use this for initialization
	void Start ()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(1);
        health.ResetHealth();
        timer = 4.0f;
        speed = 3;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!health.isAlive())
        {
            Die();
        }
        if (player != null)
        {
            Move();
        }
	}

    public override void Move()
    {
        if(timer >= 1.0f)
        {
            travel_path = Random.Range(1, 3);
            timer = 0.0f;
        }
        else if(timer >= 0.5f)
        {
            rb.velocity = Vector2.zero;
            timer += Time.deltaTime;
        }
        else
        {
            if(travel_path == 1)
            {
                if (player.position.x - transform.position.x > 0.2f)
                {
                    Vector2 movement = new Vector2(1, 0);
                    movement = movement.normalized * speed;
                    rb.velocity = movement;
                }
                else if(player.position.x - transform.position.x < -0.2f)
                {
                    Vector2 movement = new Vector2(-1, 0);
                    movement = movement.normalized * speed;
                    rb.velocity = movement;
                }
                else
                {
                    travel_path = 2;
                }
            }
            
            if(travel_path == 2)
            {
                if (player.position.y - transform.position.y > 0.2f)
                {
                    Vector2 movement = new Vector2(0, 1);
                    movement = movement.normalized * speed;
                    rb.velocity = movement;
                }
                else if (player.position.y - transform.position.y < -0.2f)
                {
                    Vector2 movement = new Vector2(0, -1);
                    movement = movement.normalized * speed;
                    rb.velocity = movement;
                }
                else
                {
                    travel_path = 1;
                }
            }
            
            timer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CiscoTesting>().health.takeDamage(1);
        }
    }

    public override void Attack()
    {
        
    }
    void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}
