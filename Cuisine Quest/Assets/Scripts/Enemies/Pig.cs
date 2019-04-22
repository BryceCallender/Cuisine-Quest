using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : EnemyAbstract
{
    Transform target;
    float minDistance = 3f;
    bool playerFound = false;
    Rigidbody2D rb;
    float timer;
    Vector2 direction = new Vector2(0, -1);
    float attackTime;
    public Weapon Sword;

    // Use this for initialization
    void Start ()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(2);
        health.ResetHealth();
        rb = GetComponent<Rigidbody2D>();
        timer = 0.0f;
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
            if(timer < 3.0f)
            {
                Vector2 movement = new Vector2(1, 0);
                movement = movement.normalized * speed;
                rb.velocity = movement;
                timer += Time.deltaTime;
            }
            else if(timer < 3.75f)
            {
                rb.velocity = Vector2.zero;
                timer += Time.deltaTime;
            }
            else if(timer < 6.75f)
            {
                Vector2 movement = new Vector2(-1, 0);
                movement = movement.normalized * speed;
                rb.velocity = movement;
                timer += Time.deltaTime;
            }
            else if(timer < 7.5f)
            {
                rb.velocity = Vector2.zero;
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0.0f;
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
            if(true)
            {
                Sword.Attack(direction);
            }
        }
    }

    void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}
