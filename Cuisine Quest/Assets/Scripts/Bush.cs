using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : EnemyAbstract
{
	// Use this for initialization
	void Start ()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(1);
        health.ResetHealth();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            health.takeDamage(1);
        }
        if (health.isAlive() == false)
        {
            Die();
        }
    }

    public override void Move()
    {
        
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
