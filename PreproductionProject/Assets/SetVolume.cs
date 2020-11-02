using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    
    public void SetLevel(float value)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(value) * 20);
    }
}
