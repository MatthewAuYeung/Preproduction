using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : BaseEnemyScript
{
    private Transform _target;
    private NavMeshAgent _agent;
    private EnemyManager _manager;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _manager = GetComponentInParent<EnemyManager>();
        _target = _manager.target;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        if(health <= 0.0f)
        {
            Destroy(gameObject);
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
                    _agent.transform.LookAt(_target.position);
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
}
