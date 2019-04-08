using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "DungeonFresh/Quest")]
public abstract class Quest : ScriptableObject {

    //public string Name;
    [TextArea]
    public string Description;
    public Quest[] DependentQuests;

    public QuestState State = QuestState.pending;
    public enum QuestState{
        pending,
        unlocked,
        inProgress,
        completed,
        done,
        canceled
    }

    //public GameObject[] ItemsNeeded;
    //public AreaScriptable AreaNeeded;
    //public GreenWeeds[] items;
}
