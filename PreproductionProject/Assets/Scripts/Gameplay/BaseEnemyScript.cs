using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemyScript : MonoBehaviour
{
    [SerializeField]
    protected float health;

    [SerializeField]
    protected float maxhealth;

    [SerializeField]
    protected float searchRange;

    [SerializeField]
    protected float attackRange;

    [SerializeField]
    protected float damage;

    [SerializeField]
    protected float attackDelay;

    [SerializeField]
    protected float fov = 60.0f;

    protected float currentTime;


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
