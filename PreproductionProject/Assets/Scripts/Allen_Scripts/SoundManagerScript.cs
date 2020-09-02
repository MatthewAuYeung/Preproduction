using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip swingSound1, swingSound2, swingSound3, swingSound4, swingSound5, runningSound, walkingSound, robotDeathSound, hurtHitSound, RobotInViewSound;
    public GameObject playerAudioGO;
    public GameObject robotAudioGO;

    public static AudioSource playerAudioSrc;
    public static AudioSource robotAudioSrc;

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
        RobotInViewSound = Resources.Load<AudioClip>("RobotEnemySig");


        playerAudioSrc = playerAudioGO.GetComponent<AudioSource>();
        robotAudioSrc = robotAudioGO.GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        bool foundPlayerClip = false;
        bool foundRobotClip = false;
        switch (clip)
        {
            case "Running":
                playerAudioSrc.clip = runningSound;
                playerAudioSrc.loop = true;
                foundPlayerClip = true;
                break;
            case "Walking":
                playerAudioSrc.clip = walkingSound;
                playerAudioSrc.loop = true;
                foundPlayerClip = true;
                break;
            case "HurtHit":
                playerAudioSrc.clip = hurtHitSound;
                playerAudioSrc.loop = false;
                foundPlayerClip = true;
                break;
            case "SwordSwing1":
                playerAudioSrc.clip = swingSound1;
                playerAudioSrc.loop = false;
                foundPlayerClip = true;
                break;
            case "SwordSwing2":
                playerAudioSrc.clip = swingSound2;
                playerAudioSrc.loop = false;
                foundPlayerClip = true;
                break;
            case "SwordSwing3":
                playerAudioSrc.clip = swingSound3;
                playerAudioSrc.loop = false;
                foundPlayerClip = true;
                break;
            case "SwordSwing4":
                playerAudioSrc.clip = swingSound4;
                playerAudioSrc.loop = false;
                foundPlayerClip = true;
                break;
            case "SwordSwing5":
                playerAudioSrc.clip = swingSound5;
                playerAudioSrc.loop = false;
                foundPlayerClip = true;
                break;
            case "RobotSig":
                robotAudioSrc.clip = RobotInViewSound;
                robotAudioSrc.loop = false;
                foundRobotClip = true;
                break;
            case "RobotDeath":
                robotAudioSrc.clip = robotDeathSound;
                robotAudioSrc.loop = false;
                foundRobotClip = true;
                break;
            default:
                break;

        }
        if (!playerAudioSrc.isPlaying && foundPlayerClip)
            playerAudioSrc.Play();
        if (!robotAudioSrc.isPlaying && foundRobotClip)
            robotAudioSrc.Play();
    }

    public static void EndPlay()
    {
        playerAudioSrc.Stop();
    }

    public static void EndPlayRobot()
    {
        robotAudioSrc.Stop();
    }
}
