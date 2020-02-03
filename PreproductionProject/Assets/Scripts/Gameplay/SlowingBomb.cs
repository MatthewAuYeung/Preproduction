using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowingBomb : MonoBehaviour
{
public float BombTimer = 2;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
            agent.speed *= 0.75f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            //rb.useGravity = false;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            Destroy(gameObject, BombTimer);
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
