using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestSystem : MonoBehaviour
{
    QuestManager questManager;
    [SerializeField]
    List<PlayerQuestData> currentQuests;
    private readonly string fileName = "Quests.json";

    // Use this for initialization
    void Start () 
    {
        questManager = FindObjectOfType<QuestManager>();
	    if(!File.Exists(Path.Combine(Application.persistentDataPath, fileName)))
        {
            currentQuests = new List<PlayerQuestData>();
            InitQuestData();
        }
        else
        {
            GetQuestsFromFile();
        }
    }
	
    /// <summary>
    /// Gets the quests from the filename specified
    /// </summary>
    private void GetQuestsFromFile()
    {
        PlayerQuestArray quests = JsonArrayHandler<PlayerQuestArray>.ReadJsonFile(Path.Combine(Application.persistentDataPath, fileName));
        foreach(var quest in quests.items)
        {
            currentQuests.Add(quest);
        }
        questManager.InitQuestScriptableObjects(quests);
    }

    /// <summary>
    /// Initializes the quest data as if the player has just started their
    /// journey in the game
    /// </summary>
    private void InitQuestData()
    {
        PlayerQuestData questData = new PlayerQuestData();
        //For all the quests we have in the manager we make 
        //a clean slate for all of them
        for(int i = 0; i < questManager.quests.Count; i++)
        {
            questData.questID = i;
            questData.questName = questManager.quests[i].questData.questName;
            questData.questState = QuestState.pending;
            questData.hasQuest = false;
            currentQuests.Add(questData);
            questData = new PlayerQuestData();
        }

        JsonArrayHandler<PlayerQuestData>.WriteJsonFile(Path.Combine(Application.persistentDataPath, fileName), currentQuests);
    }

    private void OnApplicationQuit()
    {
        JsonArrayHandler<PlayerQuestData>.WriteJsonFile(Path.Combine(Application.persistentDataPath, fileName), currentQuests);
    }
}
