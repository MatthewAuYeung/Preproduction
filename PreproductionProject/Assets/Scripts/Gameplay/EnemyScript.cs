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
    private bool isChasing = false;
    private Transform _target;
    private EnemyManager _manager;
    private WarpController _warpController;
    private SphereCollider _attackTrigger;
    private ParticleSystem _particleSystem;
    private GameObject _playerObj;
    public Signifier sign;

    public Image healthBar;
    public Image attackBar;
    public RandomLoot loot;
    private float slowSpeed;
    private float defaultSpeed;
    private float defaultAttackDelay;
    private float stunDuration;
    private float _waitTime;
    private float disBetweenPlayer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private EnemyAttackTrigger leftArm;

    [SerializeField]
    private EnemyAttackTrigger rightArm;

    [SerializeField]
    private bool isRobot;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _manager = FindObjectOfType<EnemyManager>();
        _target = _manager.target;
        _warpController = FindObjectOfType<WarpController>();
        _attackTrigger = GetComponent<SphereCollider>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _playerObj = _warpController.gameObject;
        //_sign = GetComponentInChildren<Signifier>;
        defaultSpeed = _agent.speed;
        defaultAttackDelay = attackDelay;
        meshRenderer = GetComponent<MeshRenderer>();
        originalMat = meshRenderer.material;
        isChasing = false;
    }

    private void Start()
    {
        _agent.speed = speed;
        _attackTrigger.radius = attackRange;
        _particleSystem.Pause();
        loot = FindObjectOfType<RandomLoot>();
        if (isEventTriggered)
            this.gameObject.SetActive(false);
        currentState = EnemyState.Idle;
    }

    public override void StunFromBomb(float speedModifier, float stuntEffectDuration = 5.0f)
    {
        sign.ShowStunnedSignifier();
        isStun = true;
        stunFromPlayer = false;
        stunDuration = Time.time + stuntEffectDuration;
        slowSpeed = speedModifier;
        meshRenderer.material = SlowBombEffectMat;
        //StartCoroutine(SlowFromBomb(5.0f));
    }

    public override void Stun()
    {
        sign.ShowStunnedSignifier();
        isStun = true;
        stunFromPlayer = true;
        stunDuration = Time.time + stunFromPlayerDuration;
    }

    public void SlowFromBomb(float speedModifier, float slowEffectDuration = 5.0f)
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

        if (health <= 0.0f)
        {
            SoundManagerScript.PlaySound("RobotDeath");
            Vector3 lootPosition = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
            loot.calculateLoot(lootPosition);
            _manager.enemies.Remove(this);
            transform.gameObject.SetActive(false);
            //FindObjectOfType<DoorPrefabScript>()?.RemoveEnemy(this);
            //Destroy(gameObject);

            BaseEnemyScript enemy = GetComponent<BaseEnemyScript>();
            enemy.enemyExplosion();

            NewPlayerScript.Instance.TriggerSlowMo();
            if (OnDeath != null)
            {
                OnDeath.Invoke(this);
            }
            return;
        }

        ResetHitCount();

        if (isStun)
        {
            _agent.isStopped = true;
            if (animator != null)
                animator.SetBool("isWalking", false);

            if(stunFromPlayer)
            {
                if (stunDuration < Time.time)
                {
                    isStun = false;
                }
                return;
            }
            else
            {
                if (stunDuration < Time.time)
                {
                    SlowFromBomb(slowSpeed);
                }
                return;
            }
        }

        if (beingWarpAttacked)
        {
            _agent.isStopped = true;
            if (animator != null)
                animator.SetBool("isWalking", false);
            _rb.velocity = new Vector3();
            _agent.velocity = new Vector3();
            return;
        }

        if (!isRobot)
        {
            #region oldUpdate
            disBetweenPlayer = Vector3.Distance(_agent.transform.position, _target.transform.position);
            if (disBetweenPlayer < searchRange)
            {
                if (InView())
                {
                    ChangeState(EnemyState.Chase);
                    _agent.isStopped = false;
                    if (animator != null)
                        animator.SetBool("isWalking", true);
                    _agent.SetDestination(_target.position);
                    if (disBetweenPlayer < attackRange)
                    {
                        _agent.isStopped = true;
                        if (animator != null)
                            animator.SetBool("isWalking", false);
                        Vector3 newLookPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
                        _agent.transform.LookAt(newLookPos);
                    }
                }
                else if (currentState != EnemyState.Wandering)
                {
                    StartWandering();
                    ChangeState(EnemyState.Wandering);
                }
            }
            else if (currentState != EnemyState.Wandering)
            {
                StartWandering();
                ChangeState(EnemyState.Wandering);
            }

            if (currentState == EnemyState.Wandering)
            {
                Wandering();
            }
            #endregion
        }
        else
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    Idle();
                    break;
                case EnemyState.Chase:
                    Chase();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Damaged:
                    Damaged();
                    break;
                case EnemyState.Wandering:
                    Wandering();
                    break;
                case EnemyState.NONE:
                    break;
                default:
                    break;
            }
        }
        healthBar.fillAmount = health / maxhealth;
        attackBar.fillAmount = _waitTime / attackDelay;
    }

    private void Idle()
    {
        //sign.ShowSignifier();

        if (InSearchRange() && InView())
            ChangeState(EnemyState.Chase);
        else
        {
            StartWandering();
            ChangeState(EnemyState.Wandering);
        }
    }
    private void Chase()
    {
        sign.ShowSignifier();
        if (!isChasing)
        {
            isChasing = true;
            SoundManagerScript.PlaySound("RobotSig");
        }
        _agent.isStopped = false;
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", true);
        }
        _agent.SetDestination(_target.position);
        if (InAttackRange())
        {
            _agent.isStopped = true;
            if (animator != null)
                animator.SetBool("isWalking", false);
            Vector3 newLookPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
            _agent.transform.LookAt(newLookPos);
            ChangeState(EnemyState.Attack);
        }
        if (!InSearchRange())
        {
            StartWandering();
            ChangeState(EnemyState.Wandering);
        }
    }

    private void Attack()
    {
       //sign.ShowSignifier();

        //animator.SetBool("isAttacking", true);
        if (currentTime < Time.time)
        {
            animator.SetTrigger("Attack");
            _waitTime = 0.0f;
            currentTime = Time.time + attackDelay;
        }
        else
        {
            _waitTime += Time.deltaTime;
        }
        
        if (leftArm.hit || rightArm.hit)
        {
            NewPlayerScript.Instance.TakeDamage(damage);
            leftArm.hit = false;
            rightArm.hit = false;
        }

        if (!InSearchRange())
        {
            //animator.SetBool("isAttacking", false);
            //StartWandering();
            //ChangeState(EnemyState.Wandering);
            ChangeState(EnemyState.Chase);
        }

        if (!InAttackRange())
        {
            if (InSearchRange() && InView())
            {
                ChangeState(EnemyState.Chase);
            }
        }
    }

    private void Damaged()
    {
        if (InAttackRange())
        {
            ChangeState(EnemyState.Attack);
        }
        else if (InSearchRange() && InView())
            ChangeState(EnemyState.Chase);
        else
        {
            StartWandering();
            ChangeState(EnemyState.Wandering);
        }
    }

    private void StartWandering()
    {
        if (wanderingpath == null)
            return;
        currentIndex = 0;
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", true);
        }
        _agent.SetDestination(wanderingpath.path[currentIndex].transform.position);
    }

    private void Wandering()
    {
        if (wanderingpath == null)
        {
            _agent.isStopped = true;
            if (animator != null)
                animator.SetBool("isWalking", false);
        }
        else
        {
            if (currentIndex < 0)
                currentIndex = 0;
            var dis = Vector3.Distance(_agent.transform.position, wanderingpath.path[currentIndex].transform.position);
            if (dis < OARadius)
            {
                if (!reverse)
                    currentIndex++;
                else
                    currentIndex--;
                if (currentIndex >= wanderingpath.path.Count)
                {
                    reverse = true;
                    currentIndex--;
                }
                if (currentIndex == 0)
                {
                    reverse = false;
                }

                if (currentIndex < 0)
                    currentIndex = 0;

                StartCoroutine(SetWaypoint(wanderingpath.path[currentIndex]));
            }

            if (InSearchRange() && InView())
                ChangeState(EnemyState.Chase);
        }
    }

    IEnumerator SetWaypoint(WanderingWaypoint waypoint)
    {
        if (animator != null)
            animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(waypoint.GetWaitTime());
        if (animator != null)
            animator.SetBool("isWalking", true);
        _agent.SetDestination(waypoint.transform.position);
    }

    private bool InSearchRange()
    {
        disBetweenPlayer = Vector3.Distance(_agent.transform.position, _target.transform.position);
        return (disBetweenPlayer < searchRange) ? true : false;
    }

    private bool InView()
    {
        Vector3 targetDir = _target.position - transform.position;

        float angle = Vector3.Angle(transform.forward, targetDir);


        if (angle <= fov)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, targetDir.normalized, out hit, searchRange))
            {
                if (hit.collider.gameObject.CompareTag("PlayerTag"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool InAttackRange()
    {
        disBetweenPlayer = Vector3.Distance(_agent.transform.position, _target.transform.position);
        return (disBetweenPlayer < attackRange) ? true : false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isRobot)
            return;
        if (!other.gameObject.CompareTag("PlayerTag"))
        {
            return;
        }
        if (currentTime < Time.time && InView())
        {
            if (_warpController.IsWarping())
            {
                return;
            }
            _particleSystem.Play();
            if (animator != null)
                animator.SetBool("isAttacking", true);
            _waitTime = 0.0f;
            currentTime = Time.time + attackDelay;
            other.gameObject.GetComponentInParent<NewPlayerScript>().TakeDamage(damage);
        }
        else
        {
            _waitTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isRobot)
            return;
        if (!other.gameObject.CompareTag("PlayerTag"))
        {
            return;
        }
        if (animator != null)
            animator.SetBool("isAttacking", false);
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