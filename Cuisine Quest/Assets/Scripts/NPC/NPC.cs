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

public class NPC : MonoBehaviour 
{
    public Quest giveableQuest;
    public bool hasTalked;
    [HideInInspector]
    public CharacterDialog characterDialog;
    private DialogSystemController dialogSystemController;

    private void Start()
    {
        dialogSystemController = FindObjectOfType<DialogSystemController>();
        characterDialog = GetComponent<CharacterDialog>();
    }

    //TODO::Add the quest to the player gameobject
    public void GiveQuest(CiscoTesting player)
    {
        //give quest to player and set the SO to in progress
        if(dialogSystemController.isEmpty())
        {
            Quest quest = player.GetComponent<PlayerQuestSystem>().GetQuestByID(giveableQuest.questID);
            player.GetComponent<PlayerQuestSystem>().SetQuestStatus(quest.questID,QuestState.inProgress);
            hasTalked = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasTalked)
        {
            characterDialog.EnableDialog();
        }
        //Quest is completed and we need to go to the npc to end the quest
        else if(collision.GetComponent("Player") && hasTalked)
        {
            Quest quest = collision.GetComponent<PlayerQuestSystem>().GetQuestByID(giveableQuest.questID);
            if(quest.questData.questState == QuestState.completed)
            {
                quest.questData.questState = QuestState.done;
                Debug.Log("Finished Quest");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dialogSystemController.isEmpty())
        {
            GiveQuest(collision.GetComponent<CiscoTesting>());
        }
    }
}
