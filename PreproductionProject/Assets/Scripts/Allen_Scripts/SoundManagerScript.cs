using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip swingSound1, swingSound2, swingSound3, swingSound4, swingSound5;
    public static AudioClip runningSound, walkingSound;
    public static AudioClip robotDeathSound, hurtHitSound, RobotInViewSound, RobotWalkSound;
    public static AudioClip spiderWalkSound;
    public static AudioClip laserChargingSound, laserShotSound;
    public static AudioClip warpingSound;

    public GameObject playerAudioGO;
    public GameObject playerAbAudioGO;
    public GameObject robotAudioGO;
    public GameObject spiderAudioGO;


    public static AudioSource playerAudioSrc;
    public static AudioSource playerAbAudioSrc;
    public static AudioSource robotAudioSrc;
    public static AudioSource spiderAudioSrc;

    public AudioClip swingSound1Name, swingSound2Name, swingSound3Name, swingSound4Name, swingSound5Name;
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
        laserChargingSound = Resources.Load<AudioClip>("ChargingUp");
        laserShotSound = Resources.Load<AudioClip>("LaserShot");
        warpingSound = Resources.Load<AudioClip>("Warp");


        playerAudioSrc = playerAudioGO.GetComponent<AudioSource>();
        playerAbAudioSrc = playerAbAudioGO.GetComponent<AudioSource>();
        robotAudioSrc = robotAudioGO.GetComponent<AudioSource>();
        spiderAudioSrc = spiderAudioGO.GetComponent<AudioSource>();

}

void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        bool foundPlayerClip = false;
        bool foundRobotClip = false;
        bool foundPlayerAbClip = false;
        switch (clip)
        {
            case "ChargingUp":
                playerAbAudioSrc.clip = laserChargingSound;
                playerAbAudioSrc.loop = false;
                foundPlayerAbClip = true;
                break;
            case "LaserShot":
                playerAbAudioSrc.clip = laserShotSound;
                playerAbAudioSrc.loop = false;
                foundPlayerAbClip = true;
                break;
            case "Warp":
                playerAbAudioSrc.clip = warpingSound;
                playerAbAudioSrc.loop = false;
                foundPlayerAbClip = true;
                break;
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
        if (!playerAbAudioSrc.isPlaying && foundPlayerAbClip)
            playerAbAudioSrc.Play();
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
