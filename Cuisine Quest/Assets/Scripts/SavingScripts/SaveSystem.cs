using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public List<GameObject> saveableObjects;

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
        foreach (GameObject saveObject in saveableObjects)
        {
            saveObject.GetComponent<ISaveable>().Clear();
        }
    }
}
