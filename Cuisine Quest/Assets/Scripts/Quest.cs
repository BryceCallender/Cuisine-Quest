using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Kill,
    Collect
}

[System.Serializable]
public class Quest : ISaveable 
{
    [SerializeField]
    private string questName;
    [SerializeField]
    private QuestType questType;
    [SerializeField]
    //TODO::Change the string to Required Item Class to show amount 
    //and which gameobject they will need to get
    private List<RequiredItem> requiredItems;
    [SerializeField]
    private bool completedQuest;

    public string Save()
    {
        return JsonUtility.ToJson(this,true);
    }

    bool checkQuestCompletion()
    {
        return true;
    }

}
