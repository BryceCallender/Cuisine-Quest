using System.IO;
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
        item = new ItemData();
        this.item.name = name;
        this.item.itemType = itemType;
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

    public Potion potion;

    private PlayerQuestSystem playerQuestSystem;
    public bool CheckQuests = false;
    public Dictionary<Item, int> items;
    private PlayerController playerController;

    public static string lastItemPickedUp = "";

    // Use this for initialization
    void Start ()
    {
        SaveSystem.Instance.AddSaveableObject(this);
        items = new Dictionary<Item, int>();

        if (File.Exists(Path.Combine(Application.persistentDataPath, "PlayerItems.json")))
        {
            InitDictionary();
            Debug.Log("Found save file");
        }
        else
        {
            items.Clear();
        }

        health = gameObject.AddComponent<HealthSystem>();
        playerController = GetComponent<PlayerController>();

        health.setMaxHealth(5);
        health.ResetHealth();
        playerQuestSystem = GetComponent<PlayerQuestSystem>();
	}
    bool primaryAttackButton = false;
    bool secondaryAttackButton = false;
    bool questMenuButton = false;

    // Update is called once per frame
    void Update ()
    {

        bool primaryAttackButtonDown = false;
        bool secondaryAttackButtonDown = false;

        if (Mathf.Abs(Input.GetAxisRaw("Fire1")) > 0 && !primaryAttackButton) primaryAttackButtonDown = true;
        if (Mathf.Abs(Input.GetAxisRaw("Fire2")) > 0 && !secondaryAttackButton) secondaryAttackButtonDown = true;
        //if(Mathf.Abs(Input.GetAxisRaw("")))

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentWeapon = Weapons[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentWeapon = Weapons[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentWeapon = Weapons[2];
        }

        if(Input.GetKeyDown(KeyCode.Minus))
        {
            health.takeDamage(1);
        }
        else if(Input.GetKeyDown(KeyCode.Equals))
        {

        }
    }

    void Die()
    {
        SceneManager.LoadScene("DeathScene");
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
}
