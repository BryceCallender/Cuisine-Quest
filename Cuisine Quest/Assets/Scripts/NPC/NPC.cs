using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour 
{
    public Quest giveableQuest;
    public CharacterDialog characterDialog;
    public DialogSystemController dialogSystemController;

    private void Start()
    {
        dialogSystemController = FindObjectOfType<DialogSystemController>();
    }

    //TODO::Add the quest to the player gameobject
    public void GiveQuest(CiscoTesting player)
    {
        //give quest to player and set the SO to in progress
        if(dialogSystemController.isEmpty())
        {
            Quest quest = player.GetComponent<PlayerQuestSystem>().GetQuestByID(giveableQuest.questID);
            player.GetComponent<PlayerQuestSystem>().SetQuestOn(quest.questID);
        }
    }

    public void StartDialogInteraction()
    {
        characterDialog.EnableDialog();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartDialogInteraction();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GiveQuest(collision.GetComponent<CiscoTesting>());
        }
    }

}
