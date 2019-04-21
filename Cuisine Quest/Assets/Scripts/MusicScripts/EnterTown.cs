using System.Collections;
using UnityEngine;

public class EnterTown : MonoBehaviour 
{
    public static bool isInTown;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInTown = true;

            if(isInTown)
            {
                StartCoroutine(PlayTownMusic());
            }
        }
    }

    IEnumerator PlayTownMusic()
    {
        AudioSourceController.Instance.PlayAudio("TownIntro");
        yield return new WaitForSeconds(AudioSourceController.Instance.GetLengthOfCurrentSong());
        AudioSourceController.Instance.PlayAudioLooped("TownMusic");
    }
}
