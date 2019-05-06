using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour 
{
    private void Start()
    {
        StartCoroutine(PlayCredits());
    }

    IEnumerator PlayCredits()
    {
        AudioSourceController.Instance.PlayAudio("CreditIntro");
        yield return new WaitForSeconds(AudioSourceController.Instance.GetLengthOfCurrentSong());
        AudioSourceController.Instance.PlayAudioLooped("CreditMusic");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
