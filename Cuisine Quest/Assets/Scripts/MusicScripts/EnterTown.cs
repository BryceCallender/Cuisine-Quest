using System.Collections;
using UnityEngine;

public class EnterTown : MonoBehaviour 
{
    Coroutine coroutine;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            float dot = Vector3.Dot(new Vector2(-1, 0), collision.GetComponent<Rigidbody2D>().velocity);

            if (dot > 0)
            {
                AudioSourceController.Instance.PlayAudioLooped("Town");
                StopCoroutine(coroutine);
            }
            else
            {
                coroutine = StartCoroutine(PlayFieldMusic());
            }
        }
    }

    IEnumerator PlayFieldMusic()
    {
        AudioSourceController.Instance.PlayAudio("FieldIntro");
        yield return new WaitForSeconds(AudioSourceController.Instance.GetLengthOfCurrentSong());
        AudioSourceController.Instance.PlayAudioLooped("FieldMusic");
    }
}
