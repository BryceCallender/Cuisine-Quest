using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloEnemy : EnemyAbstract
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
    float waitTimer = 0f;
    Vector3 movement;


    // Use this for initialization
    void Start()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(5);
        health.ResetHealth();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isAlive())
        {
            Die();
        }

        if (playerFound)
        {
            Move();
        }
        else if (timer > 1f)
        {
            FindPlayer();
            //for random movement if player not found
            if (waitTimer < 1f)
            {
                waitTimer += Time.deltaTime;
                movement = new Vector3(transform.position.x + UnityEngine.Random.Range(-3f, 3f), transform.position.y + UnityEngine.Random.Range(-3f, 3f));
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, movement, Time.deltaTime);
                //if it has reached it destination or ran into a wall and cannot reach its
                //destination after 3 sec move somewhere else
                if (Vector3.Distance(transform.position, movement) == 0f || waitTimer > 3f)   
                {
                    waitTimer = 0;
                }
                else
                    waitTimer += Time.deltaTime;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void FindPlayer()
    {
        foreach (RaycastHit2D collide in Physics2D.CircleCastAll(transform.position, minDistance, new Vector2(0, 0)))
        {
            if (collide.collider.CompareTag("Player"))
            {
                target = collide.transform;
                print("Found");
                playerFound = true;
                targetPos = target.position;
                thisPos = transform.position;
                //targetPos.x = targetPos.x - thisPos.x;
                //targetPos.y = targetPos.y - thisPos.y;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            collision.gameObject.GetComponent<CiscoTesting>().health.takeDamage(1);
            playerFound = false;
            timer = 0.0f;
        }
        

    }

    public override void Attack()
    {
        //throw new NotImplementedException();
    }

    public override void Move()
    {
        if (Vector3.Distance(transform.position, targetPos) == 0f)
        {
            waitTimer = 0;
            playerFound = false;
        }
        else if(waitTimer < 1f)
        {
            waitTimer += Time.deltaTime;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

    }

    void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}

