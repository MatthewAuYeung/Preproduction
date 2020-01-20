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

    public float speed = 3.5f;
    protected float currentTime;

    private float half_fov;
    private Quaternion leftRayRotation;
    private Quaternion rightRayRotation;
    private Vector3 leftDir;
    private Vector3 rightDir;

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        half_fov = fov * 0.5f;
        leftRayRotation = Quaternion.AngleAxis(-half_fov, Vector3.up);
        rightRayRotation = Quaternion.AngleAxis(half_fov, Vector3.up);

        leftDir = leftRayRotation * transform.forward;
        rightDir = rightRayRotation * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftDir * searchRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rightDir * searchRange);
        //Gizmos.color = Color.blue;
        //Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        //Gizmos.DrawRay(transform.position, direction);
    }
}
