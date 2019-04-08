using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestSystem : MonoBehaviour
{
    QuestManager questManager;
    [SerializeField]
    List<PlayerQuestData> currentQuests;
    public const int MAX_QUEST = 2;
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
    }

    /// <summary>
    /// Initializes the quest data as if the player has just started their
    /// journey in the game
    /// </summary>
    private void InitQuestData()
    {
        PlayerQuestData questData = new PlayerQuestData();

        for(int i = 0; i < MAX_QUEST; i++)
        {
            questData.questID = i;
            questData.questName = questManager.quests[i].questName;
            questData.completedQuest = false;
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
