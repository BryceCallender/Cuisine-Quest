using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CiscoTesting : MonoBehaviour 
{
    public HealthSystem health;

    public Weapon CurrentWeapon;
    public Weapon[] Weapons;

    private PlayerQuestSystem playerQuestSystem;
    public bool CheckQuests = false;
    public Dictionary<string, int> items;
    private PlayerController playerController;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start ()
    {
        health = gameObject.AddComponent<HealthSystem>();
        playerController = GetComponent<PlayerController>();
        items = new Dictionary<string, int>();
        rb = GetComponent<Rigidbody2D>();
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
        bool questMenuButtonDown = false;

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

        if(!health.isAlive())
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

        //Check for completion of the quest when an item is picked up
        if (CheckQuests)
        {
            foreach (Quest quest in playerQuestSystem.GetQuests())
            {
                if (quest.questData.questState == QuestState.inProgress)
                {
                    quest.CheckCompletion(this);
                }
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

        Debug.Log(itemName);

        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            Debug.Log("Adding " + itemName);
            items.Add(itemName, 1);
        }

        //Check for completion of the quest when an item is picked up
        if (CheckQuests)
        {
            foreach (Quest quest in playerQuestSystem.GetQuests())
            {
                playerQuestSystem.UpdateQuests(quest.questID, items);
                if (quest.questData.questState == QuestState.inProgress)
                {
                    quest.CheckCompletion(this);
                }
            }
        }
        
    }

}
