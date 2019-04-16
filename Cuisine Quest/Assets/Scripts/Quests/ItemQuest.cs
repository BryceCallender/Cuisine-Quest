using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DungeonFresh/ItemQuest")]
public class ItemQuest : Quest
{
    public AreaScriptable AreaNeeded;

    public override bool CheckCompletion(CiscoTesting player)
    {
        foreach(RequiredItem requiredItem in questData.requiredItems)
        {
            Item item = requiredItem.item.GetComponent<Item>();
            if (item != null)
            {
                //If the item is an inventory item and the name of the gameobject
                //matches that of the required item for the quest 
                if(item.Type == Item.ItemType.Inventory && item.name == requiredItem.item.name)
                {
                    player.items[requiredItem.item]--;
                    if(player.items[requiredItem.item] == 0)
                    {
                        questData.questState = QuestState.completed;
                    }
                }
            }
        }
        return questData.questState == QuestState.completed;
    }
}
