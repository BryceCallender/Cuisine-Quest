using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public List<GameObject> saveableObjects;
    public List<string> fileNames;

    private static SaveSystem instance = null;

    public static SaveSystem Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Saves the game going thru the list of saveable objects and calling 
    /// the save function which will return the json format to the object
    /// to write to the file.Once the file is saved the writer will close
    /// thus flushing the buffer into the file correctly.
    /// </summary>
    public void SaveGame()
    {
        Debug.Log("Game Saved");
        AudioSourceController.Instance.PlayAudio("Save");
        foreach (GameObject saveObject in saveableObjects)
        {
           saveObject.GetComponent<ISaveable>().Save();
        }
    }

    public void NewGame()
    {
        Debug.Log("New Game");
        foreach (string fileName in fileNames)
        {
            File.Delete(Path.Combine(Application.persistentDataPath, fileName));
        }
    }

    public void AddSaveableObject(GameObject objectToSave)
    {
        saveableObjects.Add(objectToSave);
    }
}
