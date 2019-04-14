using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioSourceController : MonoBehaviour
{
    private static AudioSourceController instance = null;

    private static Dictionary<string, AudioClip> audioClipSources;

    public static AudioSourceController Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource;
    public List<AudioClip> audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();

        audioClipSources = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in audioClips)
        {
            audioClipSources.Add(clip.name, clip);
        }
    }

    public void PlayAudio(string name)
    {
        audioSource.clip = audioClipSources[name];
        audioSource.Play();
    }
}