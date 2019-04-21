using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerItem
{
    public string name;
    public int amount;
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

    private PlayerQuestSystem playerQuestSystem;
    public Dictionary<string, int> items;
    private PlayerController playerController;

    public static string lastItemPickedUp;

    // Use this for initialization
    void Start ()
    {
        SaveSystem.Instance.AddSaveableObject(gameObject);
        items = new Dictionary<string, int>();

        if (File.Exists(Path.Combine(Application.persistentDataPath, "PlayerItems.json")))
        {
            InitDictionary();
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
	
	// Update is called once per frame
	void Update ()
    {
        PrintItems();

        if(items.Count > 0 && items != null)
        {
            playerQuestSystem.UpdateCurrentQuestsAmountDone(items);
        }

        if (!health.isAlive())
        {
            Die();
        }

        bool primaryAttack = Input.GetMouseButtonDown(0);

        if (primaryAttack && CurrentWeapon != null)
        {
            CurrentWeapon.Attack(playerController.DirectionFacing);
        }

        if (Input.GetMouseButton(0))
        {
            primaryAttack = true;
        }
        if (Input.GetMouseButtonDown(1) && CurrentWeapon != null)
        {
            CurrentWeapon.AttackSecondary(playerController.DirectionFacing, primaryAttack);
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

        //Check for completion of the quest when an item is picked up
        foreach (Quest quest in playerQuestSystem.GetQuests())
        {
            if (quest.questData.questState == QuestState.inProgress)
            {
                quest.CheckCompletion(this);
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene(0);
    }

    public void AddItem(GameObject item)
    {
        string itemName = item.name;
        if(itemName.Contains("(Clone)"))
        {
            itemName = itemName.Replace("(Clone)", "").Trim();
        }

        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            Debug.Log("Adding " + itemName);
            items.Add(itemName, 1);
            lastItemPickedUp = itemName;
        }

        //Check for completion of the quest when an item is picked up
        UpdateQuestLog();
    }

    public void RemoveItems(string name, int amount)
    {
        items[name] -= amount;
    }

    public void PrintItems()
    {
        foreach (KeyValuePair<string, int> kvp in items)
        {
            Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
        }
    }

    public void UpdateQuestLog()
    {
        if(items.Count > 0)
        {
            foreach (Quest quest in playerQuestSystem.GetQuests())
            {
                playerQuestSystem.UpdateQuests(quest.questID, items);
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

        foreach(var item in items)
        {
            PlayerItem playerItem = new PlayerItem { name = item.Key, amount = item.Value };
            playerItems.Add(playerItem);
        }

        JsonArrayHandler<PlayerItem>.WriteJsonFile(Path.Combine(Application.persistentDataPath, "PlayerItems.json"), playerItems);
    }

    public void InitDictionary()
    {
        PlayerItems playerItems = JsonArrayHandler<PlayerItems>.ReadJsonFile(Path.Combine(Application.persistentDataPath, "PlayerItems.json"));
        items.Clear();

        foreach(PlayerItem item in playerItems.items)
        {
            items.Add(item.name, item.amount);
        }
    }

}
