using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : EnemyAbstract
{
    Transform target;
    float minDistance = 3f;
    bool playerFound = false;
    Rigidbody2D rb;
    float movetimer;
    Vector2 direction = new Vector2(0, -1);
    float attackTime;
    public Weapon Sword;

    // Use this for initialization
    void Start ()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(2);
        health.ResetHealth();
        Sword = gameObject.GetComponentInChildren<Sword>();
        rb = GetComponent<Rigidbody2D>();
        movetimer = 0.0f;
        attackTime = 0.0f;
        speed = 3;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!health.isAlive())
        {
            Die();
        }

        if (rb.velocity.x > rb.velocity.y && rb.velocity.x > 0)
        {
            direction = new Vector2(1, 0);
        }
        else if (rb.velocity.x < rb.velocity.y && rb.velocity.x < 0)
        {
            direction = new Vector2(-1, 0);
        }
        else if(rb.velocity.y > rb.velocity.x && rb.velocity.y > 0)
        {
            direction = new Vector2(0, 1);
        }
        else
        {
            direction = new Vector2(0, -1);
        }
        print(direction);

        Move();
        Attack();
        if(!playerFound)
        {
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        foreach (RaycastHit2D collide in Physics2D.CircleCastAll(transform.position, minDistance, new Vector2(0, 0)))
        {
            if (collide.collider.CompareTag("Player"))
            {
                target = collide.transform;
                playerFound = true;
            }
        }
    }

    public override void Move()
    {
        if(!playerFound)
        {
            if(movetimer < 3.0f)
            {
                Vector2 movement = new Vector2(1, 0);
                movement = movement.normalized * speed;
                rb.velocity = movement;
                movetimer += Time.deltaTime;
            }
            else if(movetimer < 3.75f)
            {
                rb.velocity = Vector2.zero;
                movetimer += Time.deltaTime;
            }
            else if(movetimer < 6.75f)
            {
                Vector2 movement = new Vector2(-1, 0);
                movement = movement.normalized * speed;
                rb.velocity = movement;
                movetimer += Time.deltaTime;
            }
            else if(movetimer < 7.5f)
            {
                rb.velocity = Vector2.zero;
                movetimer += Time.deltaTime;
            }
            else
            {
                movetimer = 0.0f;
            }
            
        }
        else
        {
            Vector2 movement = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            movement = movement.normalized * speed;
            rb.velocity = movement;
        }
    }

    public override void Attack()
    {
        if(playerFound)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if(dist < 2.3f && attackTime <= 0.0f)
            {
                Sword.Attack(direction);
                attackTime = 3.0f;
            }
            else
            {
                attackTime -= Time.deltaTime;
            }
        }
    }

    void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}
