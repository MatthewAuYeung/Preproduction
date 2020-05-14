﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;


public class BombThrower : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bombParticlePrefab;
    public GameObject chargePrefab;

    public Transform handPosition;
    public Transform bombSpawnTranform;
    public float throwPower = 1500f;
    public Animator animator;
    public float downTime, upTime, pressTime = 0;
    public float countDown = 1.0f;
    public bool ready = false;
    private float waitTime;
    [SerializeField]
    private float laserSpeed = 0.25f;

    private float emptylaserSpeed;
    private NewPlayerScript _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponentInParent<NewPlayerScript>();
        chargePrefab.gameObject.transform.position = handPosition.position;
        emptylaserSpeed = laserSpeed + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!_player.HasMana(manaUsed))
        //    return;
        if (!_player.DoneCooldown(NewPlayerScript.AbilityType.Bomb))
            return;

        if (Input.GetButton("Bomb") && ready == false)
        {
            downTime = Time.time;
            if (!chargePrefab.GetComponentInChildren<ParticleSystem>().isPlaying)
                chargePrefab.GetComponentInChildren<ParticleSystem>().Play();
            if (pressTime <= countDown)
                pressTime += Time.deltaTime;
            else
            {
                pressTime = 0.0f;
                chargePrefab.GetComponentInChildren<ParticleSystem>().Stop();
                //animator.SetTrigger("Throw");
                ready = true;
            }
        }
        if (Input.GetButtonUp("Bomb"))
        {
            pressTime = 0.0f;
            ThrowBomb();
            chargePrefab.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    public void ThrowBomb()
    {
        ready = false;
        //_player.UseMana(manaUsed);
        _player.AbilityUsed(NewPlayerScript.AbilityType.Bomb);
        Vector3 throwDirection = Camera.main.transform.forward;
        Vector3 spawnPosition = /*bombSpawnTranform.position*/ handPosition.position + (throwDirection * 1f);
        //Vector3 spawnPosition = Camera.main.transform.position;
        GameObject newBomb = Instantiate(bomb, spawnPosition, Quaternion.identity);
        GameObject bombParticle = Instantiate(bombParticlePrefab, spawnPosition, Quaternion.identity);
        newBomb.GetComponent<SlowingBomb>().particleAttractor = bombParticle.GetComponentInChildren<particleAttractorLinear>().target.gameObject;
        newBomb.GetComponent<SlowingBomb>().startingPosition = spawnPosition;
        RaycastHit hit;
        if(Physics.Raycast(spawnPosition, throwDirection.normalized,out hit, 50.0f))
        {
            newBomb.transform.DOMove(hit.point, laserSpeed);
        }
        else
        {
            
            newBomb.transform.DOMove(transform.position + throwDirection.normalized * 50.0f, emptylaserSpeed).OnComplete(() => EndThrow(newBomb));
        }
        //Vector3 throwForce = throwDirection * throwPower;
        //newBomb.GetComponent<Rigidbody>().AddForce(throwForce);
        //waitTime = 0.0f;
        pressTime = 0.0f;
    }

    private void EndThrow(GameObject bomb)
    {
        var temp = bomb.GetComponent<SlowingBomb>();
        temp.DestroyOnHit();
    }
}
