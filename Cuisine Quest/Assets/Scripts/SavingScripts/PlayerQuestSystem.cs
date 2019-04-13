using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestSystem : MonoBehaviour, ISaveable
{
    QuestManager questManager;
    [SerializeField]
    public List<PlayerQuestData> currentQuests;
    private readonly string fileName = "Quests.json";
    private string filePath;

    // Use this for initialization
    void Start () 
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        questManager = FindObjectOfType<QuestManager>();
        if (!File.Exists(filePath))
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
        PlayerQuestArray quests = JsonArrayHandler<PlayerQuestArray>.ReadJsonFile(filePath);
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
            questData.amountDone = 0;
            currentQuests.Add(questData);
            questData = new PlayerQuestData();
        }

        if(File.Exists(filePath))
        {
            PlayerQuestArray quests = JsonArrayHandler<PlayerQuestArray>.ReadJsonFile(filePath);
            questManager.InitQuestScriptableObjects(quests);
        }
        Save();
    }

    public bool GetHasQuestByID(int id)
    {
        return currentQuests[id].hasQuest;
    }

    public int GetQuestCompletionStatus(int id)
    {
        return currentQuests[id].amountDone;
    }

    public Quest GetQuestByID(int id)
    {
        return questManager.quests[id];
    }

    public void SetQuestOn(int id)
    {
        currentQuests[id].hasQuest = true;
        questManager.quests[id].questData.questState = QuestState.inProgress;
    }

    public void Save()
    {
        JsonArrayHandler<PlayerQuestData>.WriteJsonFile(filePath, currentQuests);
    }

    public void Clear()
    {
        currentQuests.Clear();
        InitQuestData();
    }
}
