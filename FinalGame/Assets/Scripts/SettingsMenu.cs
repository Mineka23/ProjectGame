using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource bgmSource;

    private void Start()
    {
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
