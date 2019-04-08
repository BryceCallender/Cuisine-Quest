using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DungeonFresh/ItemQuest")]
public class ItemQuest : Quest {

    public GameObject[] ItemsNeeded;
    public AreaScriptable AreaNeeded;
}
