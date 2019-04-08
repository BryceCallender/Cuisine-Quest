using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : MonoBehaviour {

    public string Name = "Unnamed";
    public ItemType Type;
    public enum ItemType{
        Inventory,
        Weapon,
        Ability
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.GetComponent<CiscoTesting>().AddItem(Name, 1);
            GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().CurrentArea.RemoveObj(gameObject);
            //Destroy(gameObject);
        }
    }
}
