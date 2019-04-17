using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DungeonFresh/ItemQuest")]
public class ItemQuest : Quest
{
    public AreaScriptable AreaNeeded;

    public override bool CheckCompletion(CiscoTesting player)
    {
        Debug.Log(questData.requiredItems.Count);
        foreach(RequiredItem requiredItem in questData.requiredItems)
        {
            Item item = requiredItem.item.GetComponent<Item>();
            if (item != null && player.items.ContainsKey(item.name))
            {
                //If the item is an inventory item and the name of the gameobject
                //matches that of the required item for the quest 
                if(item.Type == Item.ItemType.Inventory && item.name == requiredItem.item.name)
                {
                    if (player.items[requiredItem.item.name] >= requiredItem.requiredAmount)
                    {
                        questData.questState = QuestState.completed;
                        player.items[requiredItem.item.name] -= requiredItem.requiredAmount;
                    }
                }
            }
        }
        return questData.questState == QuestState.completed;
    }
}
