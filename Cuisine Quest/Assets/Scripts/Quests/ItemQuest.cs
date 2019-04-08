using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DungeonFresh/ItemQuest")]
public class ItemQuest : Quest {

    public GameObject[] ItemsNeeded;
    public AreaScriptable AreaNeeded;

    public override void CheckCompletion(CiscoTesting player)
    {
        bool isComplete = true;
        int Lemons = player.Lemons;
        int Greens = player.Greens;
        int FishMeat = player.FishMeat;

        foreach(GameObject needed in ItemsNeeded)
        {
            Item item = needed.GetComponent<Item>();
            if (item)
            {
                if(item.Type == Item.ItemType.Inventory)
                {
                    switch (item.Name)
                    {
                        case "LemonJuice":
                            if (Lemons > 0) Lemons += -1;
                            else isComplete = false;
                            break;
                        case "Greens":
                            if (Greens > 0) Greens += -1;
                            else isComplete = false;
                            break;
                        case "FishMeat":
                            if (FishMeat > 0) FishMeat += -1;
                            else isComplete = false;
                            break;
                        default:
                            Debug.Log("Item not found.");
                            isComplete = false;
                            break;
                    }

                }
            }
        }

        if (isComplete)
        {
            State = QuestState.completed;
            player.Lemons = Lemons;
            player.Greens = Greens;
            player.FishMeat = FishMeat;
            Debug.Log("Quest Complete!");
        }
    }
}
