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

    [SerializeField]
    protected ParticleSystem hitEffect;

    [SerializeField]
    protected Material SlowBombEffectMat;

    [SerializeField, Range(0.0f, 1.0f)]
    protected float knockbackDuration;

    protected Material originalMat;
    protected MeshRenderer meshRenderer;

    public float speed = 3.5f;
    protected float currentTime;

    private float half_fov;
    private Quaternion leftRayRotation;
    private Quaternion rightRayRotation;
    private Vector3 leftDir;
    private Vector3 rightDir;
    protected Rigidbody _rb;

    protected EnemyState currentState;

    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damaged
    };

    public void ChangeState(EnemyState state)
    {
        currentState = state;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hitEffect.Play();
    }

    public void KnockBack(float amount, Vector3 point)
    {
        _rb.isKinematic = false;
        _rb.AddForceAtPosition((transform.position - point) * amount, point, ForceMode.Impulse);
        StartCoroutine(EndKnockBack());
    }

    IEnumerator EndKnockBack()
    {
        yield return new WaitForSeconds(knockbackDuration);
        _rb.isKinematic = true;
    }

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
