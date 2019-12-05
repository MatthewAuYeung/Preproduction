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

    [SerializeField]
    private float _fov = 60.0f;

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
            if (InView(_target))
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target.position);
                if (Vector3.Distance(_agent.transform.position, _target.transform.position) < attackRange)
                {
                    _agent.isStopped = true;
                    _agent.transform.LookAt(_target.position);
                }
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

        if (angle <= _fov)
            return true;
        return false;
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
