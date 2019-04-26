﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Inventory,
    Weapon,
    Ability
}

[System.Serializable]
public abstract class Item : SpawnObject 
{
    public string Name = "Unnamed";
    public ItemType Type;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.GetComponent<CiscoTesting>().AddItem(gameObject);
            //GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().CurrentArea.RemoveObj(gameObject);
            //Destroy(gameObject);
        }
    }
}
