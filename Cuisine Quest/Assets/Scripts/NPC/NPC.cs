using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCState
{
    public string name;
    public bool hasTalked;
}

[System.Serializable]
public class NPCStates
{
    public NPCState[] items;
}

/// <summary>
/// NPC behaviour. Character dialog is structured as follows. Every NPC will have
/// 3 character dialog scripts attached to them. First the Initial Quest talk
/// and any sort of dialog. The second one is what they will say upon 
/// quest completion. Lastly will be the dialog for not having done the right quests.
/// MUST BE IN THIS ORDER!
/// </summary>
public class NPC : MonoBehaviour 
{
    public List<Quest> giveableQuests;
    public bool hasTalked;
    public Item rewardItem;
    [HideInInspector]
    public CharacterDialog[] characterDialog;
    private DialogSystemController dialogSystemController;
    private PlayerController playerMovement;

    private int questsGiven;
    private int currentQuest;
    private bool completedQuest;
    private bool gaveQuest;

    void Start()
    {
        dialogSystemController = FindObjectOfType<DialogSystemController>();
        characterDialog = gameObject.GetComponents<CharacterDialog>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(dialogSystemController.isEmpty())
        {
            playerMovement.playerCanMove = true;
        }
    }

    public void GiveQuest(CiscoTesting player, int index)
    {
        //give quest to player and set the SO to in progress
        Quest quest = player.GetComponent<PlayerQuestSystem>().GetQuestByID(giveableQuests[index].questID);
        if (!CheckDependentQuests(player, quest, index) || quest.questData.questState >= QuestState.completed)
        {
            return;
        }
        //We have done them all
        player.GetComponent<PlayerQuestSystem>().SetQuestStatus(quest.questID,QuestState.inProgress);
        player.UpdateCompletionStatus();
        questsGiven++;
        gaveQuest = true;
        hasTalked = true;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        int questIndex = 0;
        Quest quest = giveableQuests[currentQuest];

        if(CheckDependentQuests(collision.gameObject.GetComponent<CiscoTesting>(), quest, questIndex))
        {
            //Enable first dialog talk
            if (collision.gameObject.CompareTag("Player") && quest.questData.questState < QuestState.completed)
            {
                characterDialog[currentQuest].EnableDialog();
                if(!gaveQuest)
                {
                    for (int i = 0; i < giveableQuests.Count; i++)
                    {
                        GiveQuest(collision.gameObject.GetComponent<CiscoTesting>(), currentQuest);
                    }
                }
            }
            //Quest is completed and we need to go to the npc to end the quest
            else
            {
                for (int i = 0; i < giveableQuests.Count; i++)
                {
                    if (quest.questData.questState == QuestState.completed)
                    {
                        //Complete the quest and enable the quest completion dialog
                        collision.gameObject.GetComponent<PlayerQuestSystem>().SetQuestStatus(quest.questID, QuestState.done);
                        gaveQuest = false;
                        currentQuest++;

                        if(rewardItem != null)
                        {
                            if (collision.gameObject.GetComponent<CiscoTesting>().items.ContainsKey(rewardItem))
                            {
                                collision.gameObject.GetComponent<CiscoTesting>().items[rewardItem]++;
                            }
                            else
                            {
                                collision.gameObject.GetComponent<CiscoTesting>().items.Add(rewardItem, 1);
                            }
                            Debug.Log("Gave a reward of " + rewardItem.Name);
                        }
                   
                        for (int j = 0; j < quest.questData.requiredItems.Count; j++)
                        {
                            RequiredItem item = quest.questData.requiredItems[j];
                            collision.gameObject.GetComponent<CiscoTesting>().RemoveItems(item.item, item.requiredAmount);
                        }
                        Debug.Log("Finished Quest");
                        characterDialog[giveableQuests.Count].EnableDialog();
                    }
                }
            }
        }
        else
        {
            characterDialog[giveableQuests.Count + 1].EnableDialog();
        }
    }

    public bool CheckDependentQuests(CiscoTesting player, Quest quest, int index)
    {
        foreach (var dependentQuest in quest.DependentQuests)
        {
            if (dependentQuest.questData.questState != QuestState.done)
            {
                return false;
            }
        }
        return true;
    }

}
