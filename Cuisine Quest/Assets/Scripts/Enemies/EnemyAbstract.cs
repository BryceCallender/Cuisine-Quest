using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstract : SpawnObject
{

    protected int health;
    public int speed;
    public droppedItem[] drops;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public abstract void Move();

    public abstract void Attack();
}

[System.Serializable]
public class droppedItem
{
    public GameObject Item;
    public int dropRate;
}

