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
    private int weaponsIndex = 0;

    private PlayerQuestSystem playerQuestSystem;
    public bool CheckQuests = false;
    public Dictionary<string, int> items;
    private PlayerController playerController;

    public static string lastItemPickedUp;

    // Use this for initialization
    void Start ()
    {
        health = gameObject.AddComponent<HealthSystem>();
        playerController = GetComponent<PlayerController>();

        health.setMaxHealth(5);
        health.ResetHealth();
        playerQuestSystem = GetComponent<PlayerQuestSystem>();

        items = new Dictionary<string, int>();

        SaveSystem.Instance.AddSaveableObject(this);
        

        if (File.Exists(Path.Combine(Application.persistentDataPath, "PlayerItems.json")))
        {
            InitDictionary();
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
        if (!playerController) playerController = GetComponent<PlayerController>();
        bool primaryAttackButtonDown = false;
        bool secondaryAttackButtonDown = false;
        bool questMenuButtonDown = false;

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

        
    }

    public void AddWeapon(Weapon newWeapon)
    {
        for (int i = 0; i < Weapons.Length; i += 1)
        {
            if (Weapons[i] == null)
            {
                Weapons[i] = newWeapon;
                newWeapon.transform.parent = transform;
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene(0);
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
        if(CheckQuests) UpdateQuestLog();
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
            if (Weapons[0] != null)
            {
                weaponsIndex = 0;
                CurrentWeapon = Weapons[weaponsIndex];
                
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Weapons[1] != null)
            {
                weaponsIndex = 1;
                CurrentWeapon = Weapons[weaponsIndex];
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Weapons[2] != null)
            {
                weaponsIndex = 2;
                CurrentWeapon = Weapons[weaponsIndex];
            }
        }
    }

}
