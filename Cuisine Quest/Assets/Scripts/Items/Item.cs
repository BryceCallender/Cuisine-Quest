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
}
