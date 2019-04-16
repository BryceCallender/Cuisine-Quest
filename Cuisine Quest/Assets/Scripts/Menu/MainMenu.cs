using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour 
{
    public void Start()
    {
        AudioSourceController.Instance.PlayAudio("MainMenu");
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
