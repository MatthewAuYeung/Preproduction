using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : MonoBehaviour
{
    public float health;

    public float range;
    public float attackRange;

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

        if (Vector3.Distance(_agent.transform.position, _target.transform.position) < range)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);
            if (Vector3.Distance(_agent.transform.position, _target.transform.position) < attackRange)
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

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
