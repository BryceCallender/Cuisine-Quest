using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {


    private int currentHealth;
    private int maxHealth = 3; //whatever amount
    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
            Die();

	}

    void Die()
    {
        //removes enemy from game
        //can leave here or move to enemy class
    }

    bool isAlive()
    {
        return (currentHealth > 0);
    }
    void takeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
    }

    void heal(int amount)
    {
        currentHealth = currentHealth + amount;
    }

    int getCurrentHealth()
    {
        return currentHealth;
    }
    void setMaxHealth(int Max)
    {
        maxHealth = Max;
    }

    void knockBack()
    {
        //add code here or move to proper class
    }
}
