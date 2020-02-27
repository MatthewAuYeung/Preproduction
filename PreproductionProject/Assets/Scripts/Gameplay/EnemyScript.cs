﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : BaseEnemyScript
{
    private Transform _target;
    private NavMeshAgent _agent;
    private EnemyManager _manager;
    private WarpController _warpController;
    private SphereCollider _attackTrigger;
    private ParticleSystem _particleSystem;

    public Image healthBar;
    float defaultSpeed;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _manager = FindObjectOfType<EnemyManager>();
        _target = _manager.target;
        _warpController = FindObjectOfType<WarpController>();
        _attackTrigger = GetComponent<SphereCollider>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        defaultSpeed = _agent.speed;
        meshRenderer = GetComponent<MeshRenderer>();
        originalMat = meshRenderer.material;
    }

    private void Start()
    {
        _agent.speed = speed;
        _attackTrigger.radius = attackRange;
        _particleSystem.Pause();
    }

    public void SlowFromBomb(float speedModifier, float effectDuration = 1f)
    {
        _agent.speed *= speedModifier;
        meshRenderer.material = SlowBombEffectMat;
        Invoke("ResetSpeed", effectDuration);
    }

    void ResetSpeed()
    {
        _agent.speed = defaultSpeed;
        meshRenderer.material = originalMat;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        if(health <= 0.0f)
        {
            _manager.enemies.Remove(this);
            transform.gameObject.SetActive(false);
            return;
            //Destroy(gameObject);
        }


        var disBetweenPlayer = Vector3.Distance(_agent.transform.position, _target.transform.position);
        if (disBetweenPlayer < searchRange)
        {
            if (InView(_target))
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target.position);
                if (disBetweenPlayer < attackRange)
                {
                    _agent.isStopped = true;
                    Vector3 newLookPos = new Vector3(_target.position.x,transform.position.y, _target.position.z);
                    _agent.transform.LookAt(newLookPos);
                }
            }
            else
            {
                _agent.isStopped = true;
            }
        }
        else
        {
            _agent.isStopped = true;
        }
        healthBar.fillAmount = health / maxhealth;


    }


    private bool InView(Transform target)
    {
        Vector3 targetDir = _target.position - transform.position;

        float angle = Vector3.Angle(transform.forward, targetDir);

        if (angle <= fov)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up,targetDir.normalized, out hit, searchRange))
            {
                if(hit.collider.gameObject.CompareTag("PlayerTag"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.gameObject.CompareTag("PlayerTag"))
        {
            return;
        }
        if (currentTime < Time.time && InView(other.gameObject.transform))
        {
            if(_warpController.IsWarping())
            {
                return;
            }
            _particleSystem.Play();
            currentTime = Time.time + attackDelay;
            other.gameObject.GetComponentInParent<NewPlayerScript>().TakeDamage(damage);
        }
    }
}
