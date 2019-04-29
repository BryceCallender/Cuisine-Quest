using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : SpawnObject 
{

    public string Name = "Unnamed";
    public ItemType Type;
    public enum ItemType
    {
        Inventory,
        Weapon,
        Ability
    }

    //private SpawningObject myOrigin;

    //public void SetMyOrigin(SpawningObject myOrigin)
    //{
    //    this.myOrigin = myOrigin;
    //}

    //public SpawningObject GetMyOrigin()
    //{
    //    return myOrigin;
    //}

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.GetComponent<CiscoTesting>().AddItem(gameObject);
            GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().CurrentArea.RemoveObj(gameObject);
            //Destroy(gameObject);
        }
    }
}
