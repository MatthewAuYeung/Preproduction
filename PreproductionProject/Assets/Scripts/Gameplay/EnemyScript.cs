using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : BaseEnemyScript
{
    public event System.Action<EnemyScript> OnDeath;
    public bool isStun = false;
    private Transform _target;
    private EnemyManager _manager;
    private WarpController _warpController;
    private SphereCollider _attackTrigger;
    private ParticleSystem _particleSystem;

    public Image healthBar;
    public Image attackBar;
    public RandomLoot loot;
    float slowSpeed;
    float defaultSpeed;
    float defaultAttackDelay;
    float stunDuration;
    float _waitTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _manager = FindObjectOfType<EnemyManager>();
        _target = _manager.target;
        _warpController = FindObjectOfType<WarpController>();
        _attackTrigger = GetComponent<SphereCollider>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        defaultSpeed = _agent.speed;
        defaultAttackDelay = attackDelay;
        meshRenderer = GetComponent<MeshRenderer>();
        originalMat = meshRenderer.material;
    }

    private void Start()
    {
        _agent.speed = speed;
        _attackTrigger.radius = attackRange;
        _particleSystem.Pause();
        loot = FindObjectOfType<RandomLoot>();
        if (isEventTriggered)
            this.gameObject.SetActive(false);
    }

    public void StunFromBomb(float speedModifier, float stuntEffectDuration = 5.0f)
    {
        isStun = true;
        stunDuration = Time.time + stuntEffectDuration;
        slowSpeed = speedModifier;
        meshRenderer.material = SlowBombEffectMat;
        //StartCoroutine(SlowFromBomb(5.0f));
    }

    public void SlowFromBomb( float speedModifier, float slowEffectDuration = 5.0f)
    {
       // yield return new WaitForSeconds(2.0f);
        isStun = false;
        _agent.speed *= speedModifier;
        attackDelay /= speedModifier;
        meshRenderer.material = SlowBombEffectMat;
        Invoke("ResetSpeed", slowEffectDuration);
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
            Vector3 lootPosition = new Vector3(transform.position.x, 0.3f, transform.position.z);
            loot.calculateLoot(lootPosition);
            _manager.enemies.Remove(this);
            transform.gameObject.SetActive(false);
            //FindObjectOfType<DoorPrefabScript>()?.RemoveEnemy(this);
            //Destroy(gameObject);
            OnDeath.Invoke(this);
            return;
        }

        if (isStun)
        {
            _agent.isStopped = true;
            if(stunDuration < Time.time)
            {
                SlowFromBomb(slowSpeed);
            }
            return;
        }

        if (beingWarpAttacked)
        {
            _agent.isStopped = true;
            _rb.velocity = new Vector3();
            _agent.velocity = new Vector3();
            return;
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
            else if(currentState != EnemyState.Wandering)
            {
                currentIndex = 0;
                _agent.SetDestination(wanderingpath.path[currentIndex].transform.position);
                ChangeState(EnemyState.Wandering);
            }
        }
        else if(currentState != EnemyState.Wandering)
        {
            currentIndex = 0;
            _agent.SetDestination(wanderingpath.path[currentIndex].transform.position);
            ChangeState(EnemyState.Wandering);
        }

        if(currentState == EnemyState.Wandering)
        {
            Wandering();
        }

        healthBar.fillAmount = health / maxhealth;
        attackBar.fillAmount = _waitTime / attackDelay;

    }

    private void Wandering()
    {
        if (wanderingpath == null)
            _agent.isStopped = true;
        else
        {
            Debug.Log(Vector3.Distance(_agent.transform.position, wanderingpath.path[currentIndex].transform.position));
            if(Vector3.Distance(_agent.transform.position, wanderingpath.path[currentIndex].transform.position) < 0.5f)
            {
                currentIndex++;
                if (currentIndex >= wanderingpath.path.Count)
                    currentIndex = 0;
                _agent.SetDestination(wanderingpath.path[currentIndex].transform.position);
                //lastPos = wanderingpath.path[currentIndex].transform.position;
            }
            
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
            _waitTime = 0.0f;
            currentTime = Time.time + attackDelay;
            other.gameObject.GetComponentInParent<NewPlayerScript>().TakeDamage(damage);
        }
        else
        {
            _waitTime += Time.deltaTime;
        }
    }

    public void KnockBack(float amount, Vector3 point)
    {
        _rb.isKinematic = false;
        _rb.AddForceAtPosition((transform.position - point) * amount, point, ForceMode.Impulse);
        StartCoroutine(EndKnockBack());
    }

    IEnumerator EndKnockBack()
    {
        yield return new WaitForSeconds(0.5f);
        _rb.isKinematic = true;
    }
}
