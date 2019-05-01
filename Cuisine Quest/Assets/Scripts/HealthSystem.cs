﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour 
{
    public int currentHealth;
    public int maxHealth = 3; //whatever amount
    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {
       

	}

    public void Die()
    {
        //removes enemy from game
        //can leave here or move to enemy class
    }

    public bool isAlive()
    {
        return (currentHealth > 0);
    }
    public virtual void takeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void heal(int amount)
    {
        currentHealth = currentHealth + amount;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setMaxHealth(int Max)
    {
        maxHealth = Max;
    }

    public void knockBack()
    {
        //add code here or move to proper class
    }
}
