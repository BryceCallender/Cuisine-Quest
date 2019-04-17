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
/// 2 character dialog scripts attached to them. First the Initial Quest talk
/// and any sort of dialog. The second one is what they will say upon 
/// quest completion. MUST BE IN THIS ORDER!
/// </summary>
public class NPC : MonoBehaviour 
{
    public List<Quest> giveableQuests;
    public bool hasTalked;
    [HideInInspector]
    public CharacterDialog[] characterDialog;
    private DialogSystemController dialogSystemController;

    private void Start()
    {
        dialogSystemController = FindObjectOfType<DialogSystemController>();
        characterDialog = gameObject.GetComponents<CharacterDialog>();
    }

    //TODO::Add the quest to the player gameobject
    public void GiveQuest(CiscoTesting player, int index)
    {
        //give quest to player and set the SO to in progress
        if(dialogSystemController.isEmpty())
        {
            Quest quest = player.GetComponent<PlayerQuestSystem>().GetQuestByID(giveableQuests[index].questID);
            player.GetComponent<PlayerQuestSystem>().SetQuestStatus(quest.questID,QuestState.inProgress);
            hasTalked = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Enable first dialog talk
        if(collision.CompareTag("Player") && !hasTalked)
        {
            characterDialog[0].EnableDialog();
        }
        //Quest is completed and we need to go to the npc to end the quest
        else if(collision.CompareTag("Player") && hasTalked)
        {
            for (int i = 0; i < giveableQuests.Count; i++)
            {
                Quest quest = collision.GetComponent<PlayerQuestSystem>().GetQuestByID(giveableQuests[i].questID);
                if (quest.questData.questState == QuestState.completed)
                {
                    //Complete the quest and enable the quest completion dialog
                    collision.GetComponent<PlayerQuestSystem>().SetQuestStatus(quest.questID, QuestState.done);
                    Debug.Log("Finished Quest");
                    characterDialog[1].EnableDialog();
                }
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            for (int i = 0; i < giveableQuests.Count; i++)
            {
                Quest quest = collision.GetComponent<PlayerQuestSystem>().GetQuestByID(giveableQuests[i].questID);
                if (dialogSystemController.isEmpty()
                    && quest.questData.questState < QuestState.completed)
                {
                    GiveQuest(collision.GetComponent<CiscoTesting>(),i);
                }
            }

        }

    }
}
