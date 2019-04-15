using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : EnemyAbstract
{

	// Use this for initialization
	void Start ()
    {
        health = 1;
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
