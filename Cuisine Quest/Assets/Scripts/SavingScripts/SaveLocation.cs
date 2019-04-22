using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLocation : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("SAVING THE GAME");
            AudioSourceController.Instance.PlayAudio("Save");
            SaveSystem.Instance.SaveGame();
        }
    }
}
