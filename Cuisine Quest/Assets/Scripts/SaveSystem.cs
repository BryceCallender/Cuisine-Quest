using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public List<GameObject> saveableObjects;
    public List<Quest> quests;
    private StreamWriter saveFileWriter;
    private StreamReader saveFileReader;
    private string path;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "CuisineQuest.save");
        //Opens the stream writer to the file path and Path.combine
        //guarentees that we will have the right path written by the OS
        saveFileWriter = new StreamWriter(path);
        //TODO::Remove this and put elsewhere this is just for testing
        SaveGame();
        //LoadGame();
    }

    /*
     * Saves the game going thru the list of saveable objects and calling 
     * the save function which will return the json format to the object
     * to write to the file. Once the file is saved the writer will close
     * thus flushing the buffer into the file correctly.   
     */
    public void SaveGame()
    {
        //foreach(GameObject saveObject in saveableObjects)
        //{
        //    print("Saved File");
        //    saveFileWriter.Write(saveObject.GetComponent<ISaveable>().Save());
        //}

        foreach (Quest quest in quests)
        {
            print("Saved File");
            saveFileWriter.Write(quest.Save());
        }

        saveFileWriter.Close();
    }

    public void LoadGame()
    {
        saveFileReader = new StreamReader(path);
        JsonUtility.FromJson<Quest>(saveFileReader.ReadToEnd());
    }
}
