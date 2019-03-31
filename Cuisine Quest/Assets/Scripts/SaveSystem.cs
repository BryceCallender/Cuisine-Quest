using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public List<GameObject> saveableObjects;
    private StreamWriter saveFileWriter;
    private StreamReader saveFileReader;
    private string path;

    private void Start()
    {
        //Opens the stream writer to the file path and Path.combine
        //guarentees that we will have the right path written by the OS
        path = Path.Combine(Application.persistentDataPath, "CuisineQuest.save");

        if(File.Exists(path))
        {
            Debug.Log("Save file exists");
        }
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
        print("Saved File");
        foreach (GameObject saveObject in saveableObjects)
        {
           saveFileWriter.Write(saveObject.GetComponent<ISaveable>().Save());
        }
        saveFileWriter.Close();
    }

    public void LoadGame()
    {
        saveFileReader = new StreamReader(path);
        JsonUtility.FromJson<QuestData>(saveFileReader.ReadToEnd());
    }
}
