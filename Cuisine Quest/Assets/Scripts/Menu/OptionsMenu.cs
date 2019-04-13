using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour 
{
    public TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;
    public AudioMixer audioMixer;

    private void Start()
    {
        resolutions = Screen.resolutions;
        SetResolutionDropDown();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolutionDropDown()
    {
        List<string> screenResolutions = new List<string>();

        resolutionDropDown.ClearOptions();

        foreach(Resolution res in resolutions)
        {
            screenResolutions.Add(res.width + " x " + res.height);
        }

        resolutionDropDown.AddOptions(screenResolutions);
    }
}
