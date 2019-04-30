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
    [SerializeField]
    private bool isTalking;
    [HideInInspector]
    public CharacterDialog[] characterDialog;
    private DialogSystemController dialogSystemController;
    private PlayerController playerMovement;

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
        if (!CheckDependentQuests(player, quest, index))
        {
            return;
        }
        //We have done them all
        player.GetComponent<PlayerQuestSystem>().SetQuestStatus(quest.questID,QuestState.inProgress);
        player.UpdateCompletionStatus();
        hasTalked = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        int questIndex = 0;
        foreach (var quest in giveableQuests)
        {
            if(CheckDependentQuests(collision.GetComponent<CiscoTesting>(), quest, questIndex))
            {
                //Enable first dialog talk
                if (collision.CompareTag("Player") && !hasTalked)
                {
                    characterDialog[0].EnableDialog();
                    isTalking = true;
                    GiveQuest(collision.GetComponent<CiscoTesting>(), 0);
                }
                //Quest is completed and we need to go to the npc to end the quest
                else if (collision.CompareTag("Player") && hasTalked)
                {
                    for (int i = 0; i < giveableQuests.Count; i++)
                    {
                        if (quest.questData.questState == QuestState.completed)
                        {
                            //Complete the quest and enable the quest completion dialog
                            collision.GetComponent<PlayerQuestSystem>().SetQuestStatus(quest.questID, QuestState.done);
                            for (int j = 0; j < quest.questData.requiredItems.Count; j++)
                            {
                                RequiredItem item = quest.questData.requiredItems[j];
                                collision.GetComponent<CiscoTesting>().RemoveItems(item.item, item.requiredAmount);
                            }
                            Debug.Log("Finished Quest");
                            characterDialog[1].EnableDialog();
                            isTalking = true;
                        }
                    }
                }
            }
            else
            {
                characterDialog[2].EnableDialog();
                isTalking = true;
            }
            questIndex++;
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
