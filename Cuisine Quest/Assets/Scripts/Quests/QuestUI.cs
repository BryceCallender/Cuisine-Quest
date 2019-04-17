using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour 
{
    QuestManager questManager;
    PlayerQuestSystem playerQuestSystem;
    public GUIStyle gUIStyle;
    private bool showQuestUI;

    const int offset = 20;
    const int padding = 5;
    const int bottomScreenDisplacement = 50; //Nice number and part of it comes from how long the hearts on the screen is
    readonly Vector2 GUI_BOX_SIZE = new Vector2(200, 30);

    //The amount of things for IMGUI to draw
    int guiQuestcount;

    // The position on of the scrolling viewport
    private Vector2 scrollPosition = Vector2.zero;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        playerQuestSystem = FindObjectOfType<PlayerQuestSystem>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            showQuestUI = !showQuestUI;
        }

        guiQuestcount = playerQuestSystem.GetActiveQuestCount();
    }

    private void OnGUI()
    {
        if(showQuestUI)
        {
            List<Quest> quests = questManager.GetQuests();
            //Draw Big Box to hold all the quests
            GUI.Box(new Rect(10,40,250,Screen.height - bottomScreenDisplacement),"Quests");
            int placeX = 15;
            int placeY = 60;
            foreach (Quest quest in quests)
            {
                if (playerQuestSystem.GetHasQuestByID(quest.questID) &&
                    (quest.questData.questState == QuestState.inProgress ||
                     (quest.questData.questState == QuestState.completed)))
                {
                    scrollPosition = GUI.BeginScrollView(new Rect(10, 40, 250, Screen.height - bottomScreenDisplacement), scrollPosition, new Rect(10, 40, 200, 75 * guiQuestcount),false,false);

                    //Draw the name of the quest
                    GUI.Label(new Rect(placeX, placeY, 100, 20), quest.questData.questName);
                    //Move the placement down by the offset indication
                    placeY += offset;

                    //Check if the quest is completed or not in order to say 
                    //to collect more or just display COMPLETED!
                    GUI.Box(new Rect(placeX, placeY, GUI_BOX_SIZE.x, GUI_BOX_SIZE.y),"");
                    if (quest.questData.questState == QuestState.completed)
                    {
                        //Just display that the quest has been completed
                        GUI.Label(new Rect(placeX + padding, placeY + padding, GUI_BOX_SIZE.x, GUI_BOX_SIZE.y), "Completed Quest!");
                    }
                    else if(quest.questData.questState == QuestState.inProgress)
                    {
                        //Draw the numbers of stuff left for the quest
                        string completionString = string.Format("{0}: ", quest.questData.requiredItems[0].item.name);
                        completionString += playerQuestSystem.GetQuestCompletionStatus(quest.questID).ToString();
                        completionString += "/" + quest.questData.requiredItems[0].requiredAmount;
                        GUI.Label(new Rect(placeX + padding, placeY + padding, GUI_BOX_SIZE.x, GUI_BOX_SIZE.y),completionString);
                    }

                    //Move the placement down by the offset indication
                    placeY += offset + (padding * 2);

                    // End the scroll view that we began above.
                    GUI.EndScrollView();
                }
            }
        }
    }
}
