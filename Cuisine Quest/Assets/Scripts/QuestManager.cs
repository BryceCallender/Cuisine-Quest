using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    /// <summary>
    /// Makes this class non-destroyable. It also initializes the quest data
    /// by reading the text file with all the attributes for the game quests
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(this);
        //ReadQuestFile();
    }

    public void InitQuestScriptableObjects(PlayerQuestArray playerData)
    {
        int index = 0;
        foreach(var quest in playerData.items)
        {
            quests[index].questData.questState = quest.questState;
            index++;
        }
    }

    /// <summary>
    /// Reads the quest file.
    /// </summary>
    //public void ReadQuestFile()
    //{
        //StreamReader questFileReader = new StreamReader(Path.Combine(Application.dataPath,"Quests.txt"));
        ////Make file readonly
        //File.SetAttributes(Path.Combine(Application.dataPath, "Quests.txt"), FileAttributes.ReadOnly);

        //string line;
        //QuestData questData = new QuestData();
        //while ((line = questFileReader.ReadLine()) != null)
        //{
        //    Debug.Log(line);
        //    switch (line)
        //    {
        //        case "NAME":
        //            //Reads name of quest
        //            questData.questName = questFileReader.ReadLine();
        //            break;
        //        case "BEGINDESC":
        //            string description;
        //            //Just reads the file till it hits the Quest Type indicator
        //            //since the description can be multiple lines
        //            while(!(description = questFileReader.ReadLine()).Contains("ENDDESC")) 
        //            {
        //                questData.description += description;
        //            }
        //            break;
        //        case "QUESTTYPE":
        //            //Gets the quest type using TryParse which will convert
        //            //from string to int if possible and store into the 
        //            //out variable
        //            int questType;
        //            int.TryParse(questFileReader.ReadLine(), out questType);
        //            questData.questType = (QuestType)questType;
        //            break;
        //        case "REQUIRED":
        //            //Reads the required items for the quest and will use a 
        //            //similar way of reading string to int as the previous 
        //            //questtype way.
        //            int numberOfRequirements;
        //            int.TryParse(questFileReader.ReadLine(), out numberOfRequirements);
        //            RequiredItem requiredItem = new RequiredItem();
        //            for (int num = 0; num < numberOfRequirements; num++)
        //            {
        //                requiredItem.item = questFileReader.ReadLine();
        //                int amount;
        //                int.TryParse(questFileReader.ReadLine(), out amount);
        //                requiredItem.requiredAmount = amount;

        //                questData.requiredItems.Add(requiredItem);
        //            }
        //            break;
        //        case "END":
        //            //End of the quest so we store the quest into the list
        //            //and create a new object to read into
        //            quests.Add(questData);
        //            questData = new QuestData();
        //            break;
        //    }
        //}
    //}
}
