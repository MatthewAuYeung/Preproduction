using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip swingSound1, swingSound2, swingSound3, swingSound4, swingSound5, runningSound, walkingSound, robotDeathSound, hurtHitSound;
    static AudioSource audioSrc;

    void Start()
    {
        swingSound1 = Resources.Load<AudioClip>("SwordSwing1");
        swingSound2 = Resources.Load<AudioClip>("SwordSwing2");
        swingSound3 = Resources.Load<AudioClip>("SwordSwing3");
        swingSound4 = Resources.Load<AudioClip>("SwordSwing4");
        swingSound5 = Resources.Load<AudioClip>("SwordSwing5");
        runningSound = Resources.Load<AudioClip>("Running");
        walkingSound = Resources.Load<AudioClip>("Walking2");
        robotDeathSound = Resources.Load<AudioClip>("RobotDeath");
        hurtHitSound = Resources.Load<AudioClip>("HurtHit");

        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        bool foundClip = true;
        switch (clip)
        {
            case "Running":
                audioSrc.clip = runningSound;
                audioSrc.loop = true;
                break;
            case "Walking":
                audioSrc.clip = walkingSound;
                audioSrc.loop = true;
                break;
            case "RobotDeath":
                audioSrc.PlayOneShot(robotDeathSound);
                break;
            case "HurtHit":
                audioSrc.PlayOneShot(hurtHitSound);
                break;
            case "SwordSwing1":
                audioSrc.clip = swingSound1;
                audioSrc.loop = false;
                break;
            case "SwordSwing2":
                audioSrc.clip = swingSound2;
                audioSrc.loop = false;
                break;
            case "SwordSwing3":
                audioSrc.clip = swingSound3;
                audioSrc.loop = false;
                break;
            case "SwordSwing4":
                audioSrc.clip = swingSound4;
                audioSrc.loop = false;
                break;
            case "SwordSwing5":
                audioSrc.clip = swingSound5;
                audioSrc.loop = false;
                break;
            default:
                foundClip = false;
                break;

        }
        if (!audioSrc.isPlaying && foundClip)
            audioSrc.Play();
    }

    public static void EndPlay()
    {
        audioSrc.Stop();
    }
}
