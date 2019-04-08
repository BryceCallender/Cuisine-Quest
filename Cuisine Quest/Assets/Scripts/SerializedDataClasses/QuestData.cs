using System.Collections.Generic;

/// <summary>
/// Quest types. There is kill, collect, and kill and collect types of quest
/// available.
/// </summary>
public enum QuestType
{
    Kill,
    Collect,
    KillAndCollect
}

/// <summary>
/// Quest data. Each quest has a name,description,type,items needed, and a 
/// completion status
/// </summary>
[System.Serializable]
public class QuestData 
{
    public string questName;
    public string description;
    public QuestType questType;
    public List<RequiredItem> requiredItems = new List<RequiredItem>();
    public bool completedQuest;
}

/// <summary>
/// Player quest data that will be used to write to a json file.
/// </summary>
[System.Serializable]
public class PlayerQuestData
{
    public int questID;
    public string questName;
    public bool hasQuest;
    public bool completedQuest;
}

/// <summary>
/// Array of playerquestdata in order to read multiple objects from the 
/// same json file
/// </summary>
[System.Serializable]
public class PlayerQuestArray
{
    public PlayerQuestData[] items;
}
