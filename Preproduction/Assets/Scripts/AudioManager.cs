using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Start()
    {
        // Looop the BGM
        musicSource.loop = true;
    }

    public void SetMasterVolume(float vol)
    {
        audioMixer.SetFloat("Master_volume", vol);
    }

    public void SetMusicVolume(float vol)
    {
        audioMixer.SetFloat("Music_volume", vol);
    }

    public void SetSFXVolume(float vol)
    {
        audioMixer.SetFloat("SFX_volume", vol);
    }
}
