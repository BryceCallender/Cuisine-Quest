﻿using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerItem
{
    public ItemData item;
    public int amount;

    public PlayerItem(string name, ItemType itemType, int amount)
    {
        item = new ItemData
        {
            name = name,
            itemType = itemType
        };
        this.amount = amount;
    }
}

[System.Serializable]
public class ItemData
{
    public string name;
    public ItemType itemType;
}

[System.Serializable]
public class PlayerItems
{
    public PlayerItem[] items;
}


public class CiscoTesting : MonoBehaviour, ISaveable
{
    public HealthSystem health;

    public Weapon CurrentWeapon;
    public Weapon[] Weapons;
    private int weaponsIndex = 0;

    public Potion potion;

    private PlayerQuestSystem playerQuestSystem;
    public bool CheckQuests = false;
    public Dictionary<Item, int> items;
    private PlayerController playerController;

    // Use this for initialization
    void Start ()
    {
        SaveSystem.Instance.AddSaveableObject(this);
        items = new Dictionary<Item, int>();
        health = gameObject.AddComponent<HealthSystem>();
        playerController = GetComponent<PlayerController>();

        health.setMaxHealth(5);
        health.ResetHealth();
        playerQuestSystem = GetComponent<PlayerQuestSystem>();


        if (File.Exists(Path.Combine(Application.persistentDataPath, "PlayerItems.json")))
        {
            InitDictionary();
            Debug.Log("Found save file");
        }
        else
        {
            items.Clear();
        }

        
	}
    bool primaryAttackButton = false;
    bool secondaryAttackButton = false;
    bool questMenuButton = false;

    // Update is called once per frame
    void Update ()
    {
        if(PauseMenu.paused)
        {
            return;
        }

        if (!playerController) playerController = GetComponent<PlayerController>();
        bool primaryAttackButtonDown = false;
        bool secondaryAttackButtonDown = false;

        if (Mathf.Abs(Input.GetAxisRaw("Fire1")) > 0 && !primaryAttackButton) primaryAttackButtonDown = true;
        if (Mathf.Abs(Input.GetAxisRaw("Fire2")) > 0 && !secondaryAttackButton) secondaryAttackButtonDown = true;
        //if(Mathf.Abs(Input.GetAxisRaw("")))

        handleWeaponSwitching();

        if (Mathf.Abs(Input.GetAxisRaw("Fire1")) > 0)
        {
            primaryAttackButton = true;
        }
        else
        {
            primaryAttackButton = false;
        }

        if (Mathf.Abs(Input.GetAxisRaw("Fire2")) > 0)
        {
            secondaryAttackButton = true;
        }
        else
        {
            secondaryAttackButton = false;
        }

        if(secondaryAttackButtonDown) Debug.Log(primaryAttackButton + " " + secondaryAttackButtonDown);

        if(items.Count > 0 && items != null)
        {
            playerQuestSystem.UpdateCurrentQuestsAmountDone(items);
        }

        if (!health.isAlive())
        {
            Die();
        }

        //bool primaryAttack = Input.GetMouseButtonDown(0);
        //bool secondaryAttack = secondaryAttackButton;

        if (primaryAttackButtonDown && CurrentWeapon != null)
        {
            CurrentWeapon.Attack(playerController.DirectionFacing);
        }
        
        //if (Input.GetMouseButton(0))
        //{
        //    primaryAttack = true;
        //}
        if (secondaryAttackButtonDown && CurrentWeapon != null)
        {
            CurrentWeapon.AttackSecondary(playerController.DirectionFacing, primaryAttackButton);
        }

        if(Input.GetKeyDown(KeyCode.Minus))
        {
            health.takeDamage(1);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(items[potion] > 0)
            {
                potion.Consume(this);
                items[potion]--;
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene("DeathScene");
    }

    public void ChangeLayer(int Layer, int orderInLayer){

        gameObject.layer = Layer;
        GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + orderInLayer;

        foreach(Weapon w in Weapons){
            w.gameObject.layer = Layer;
            w.Mesh.gameObject.layer = Layer;
            w.Mesh.GetComponent<SpriteRenderer>().sortingOrder = w.Mesh.GetComponent<SpriteRenderer>().sortingOrder + orderInLayer;
        }
    }
    public void AddItem(GameObject item)
    {
        if (items.ContainsKey(item.GetComponent<Item>()))
        {
            items[item.GetComponent<Item>()]++;
        }
        else
        {
            items.Add(item.GetComponent<Item>(), 1);
        }

        //Check for completion of the quest when an item is picked up
        UpdateQuestLog(item.GetComponent<Item>());
        item.SetActive(false);
        
        //if(CheckQuests) UpdateQuestLog();
    }

    public void RemoveItems(Item item, int amount)
    {
        items[item] -= amount;
        UpdateQuestLog(item);
    }

    public void PrintItems()
    {
        foreach (KeyValuePair<Item, int> kvp in items)
        {
            Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
        }
    }

    public void UpdateCompletionStatus()
    {
        if (items.Count > 0)
        {
            foreach (Quest quest in playerQuestSystem.GetQuests())
            {
                if (quest.questData.questState == QuestState.inProgress ||
                    quest.questData.questState == QuestState.completed)
                {
                    quest.CheckCompletion(this);
                }
            }
        }
    }

    public void UpdateQuestLog(Item item)
    {
        if (items.Count > 0)
        {
            foreach (Quest quest in playerQuestSystem.GetQuests())
            {
                playerQuestSystem.UpdateQuests(quest.questID, items, item);
                if (quest.questData.questState == QuestState.inProgress ||
                    quest.questData.questState == QuestState.completed)
                {
                    quest.CheckCompletion(this);
                }
            }
        }
    }

    public void Save()
    {
        List<PlayerItem> playerItems = new List<PlayerItem>();

        foreach (var itemObj in items)
        {
            Debug.Log(itemObj);
            PlayerItem playerItem = new PlayerItem(itemObj.Key.Name, itemObj.Key.Type, itemObj.Value);
            playerItems.Add(playerItem);
        }

        JsonArrayHandler<PlayerItem>.WriteJsonFile(Path.Combine(Application.persistentDataPath, "PlayerItems.json"), playerItems);
    }

    public void InitDictionary()
    {
        PlayerItems playerItems = JsonArrayHandler<PlayerItems>.ReadJsonFile(Path.Combine(Application.persistentDataPath, "PlayerItems.json"));
        items.Clear();

        foreach (PlayerItem item in playerItems.items)
        {
            GameObject go = new GameObject();
            go.AddComponent<Item>();
            Item gameObjectItem = go.GetComponent<Item>();
            gameObjectItem.Name = item.item.name;
            gameObjectItem.Type = item.item.itemType;

            items.Add(gameObjectItem, item.amount);
        }
    }

    bool weaponSelectIncrease = false;
    bool weaponSelectDecrease = false;

    private void handleWeaponSwitching()
    {
        bool weaponSelectIncreaseDown = false;
        bool weaponSelectDecreaseDown = false;

        if (Input.GetAxis("WeaponSelectIncrease") > 0.5 && !weaponSelectIncrease) weaponSelectIncreaseDown = true;
        if (Input.GetAxis("WeaponSelectDecrease") > 0.5 && !weaponSelectDecrease) weaponSelectDecreaseDown = true;

        if (Input.GetAxis("WeaponSelectIncrease") > 0.5) weaponSelectIncrease = true;
        else weaponSelectIncrease = false;
        if (Input.GetAxis("WeaponSelectDecrease") > 0.5) weaponSelectDecrease = true;
        else weaponSelectDecrease = false;


        if (weaponSelectIncreaseDown)
        {
            weaponsIndex = (weaponsIndex + 1) % Weapons.Length;
            CurrentWeapon = Weapons[weaponsIndex];
        }
        if (weaponSelectDecreaseDown)
        {
            weaponsIndex = (weaponsIndex - 1) ;
            if (weaponsIndex < 0) weaponsIndex = Weapons.Length - 1;
            CurrentWeapon = Weapons[weaponsIndex];
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Weapons.Length > 0)
            {
                weaponsIndex = 0;
                CurrentWeapon = Weapons[weaponsIndex];

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Weapons.Length > 1)
            {
                weaponsIndex = 1;
                CurrentWeapon = Weapons[weaponsIndex];
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Weapons.Length > 2)
            {
                weaponsIndex = 2;
                CurrentWeapon = Weapons[weaponsIndex];
            }
        }
    }
}
