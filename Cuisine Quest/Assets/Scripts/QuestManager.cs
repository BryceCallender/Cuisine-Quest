using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    private List<QuestData> quests;

    public string Save()
    {
        string jsonString = "";

        foreach(QuestData data in quests)
        {
            jsonString += JsonUtility.ToJson(data, true);
        }
        return jsonString;
    }
}
