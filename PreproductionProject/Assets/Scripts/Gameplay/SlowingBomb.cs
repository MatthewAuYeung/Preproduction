using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowingBomb : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
            agent.speed *= 0.75f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
            BaseEnemyScript enemy = other.gameObject.GetComponent<BaseEnemyScript>();
            agent.speed = enemy.speed;
        }
    }
}
