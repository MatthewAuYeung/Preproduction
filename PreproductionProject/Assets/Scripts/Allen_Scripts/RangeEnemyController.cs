using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class RangeEnemyController : BaseEnemyScript
{
    public event System.Action<EnemyScript> OnDeath;
    private EnemyManager _manager;
    private Transform _target;
    private WarpController _warpController;
    public Transform target, aim, head;
    public float reloadTime = 1.0f;
    public float turnSpeed = 5.0f;
    public float firePauseTime = 0.25f;
    public float range = 5.0f;
    public float rangeEnemyDamage = 10.0f;
    public Transform[] muzzlePos;
    public bool canSee = false;
    public GameObject muzzleFlash;
    public Image healthBar;
    public RandomLoot loot;
    private Animator ani;
    public GameObject LaserPrefab;

    private float nextFireTime;
    private float nextMoveTime;
    public int randomMuzzel;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _manager = FindObjectOfType<EnemyManager>();
        _target = _manager.target;
        _warpController = FindObjectOfType<WarpController>();
        ani = GetComponent<Animator>();

    }
    void Start()
    {
        muzzleFlash.SetActive(false);
        if (isEventTriggered)
            this.gameObject.SetActive(false);
    }

    public float GetNextFireTime()
    {
        return nextFireTime;
    }
    public float GetReloadTime()
    {
        return reloadTime;
    }

    void Update()
    {
        Tracking();
        AimFire();
        if (health <= 0.0f)
        {
            transform.gameObject.SetActive(false);
            return;
        }

        if (beingWarpAttacked)
        {
            _agent.isStopped = true;
            _rb.velocity = new Vector3();
            _agent.velocity = new Vector3();
            return;
        }

        healthBar.fillAmount = health / maxhealth;

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    OpenTurret();
        //}
    }

    void AimFire()
    {
        if (target)
        {
            if (Time.time >= nextMoveTime)
            {
                aim.LookAt(target);
                aim.eulerAngles = new Vector3(aim.eulerAngles.x, aim.eulerAngles.y, 0);
                head.rotation = Quaternion.Lerp(head.rotation, aim.rotation, Time.deltaTime * turnSpeed);
            }

            if (Time.time >= nextFireTime && canSee)
            {
                Fire();
            }
            else
            {
                muzzleFlash.SetActive(false);
            }
        }

        if (target == null)
            muzzleFlash.SetActive(false);
    }

    void Fire()
    {
        randomMuzzel = Random.Range(0, muzzlePos.Length);
        nextFireTime = Time.time + reloadTime;
        nextMoveTime = Time.time + firePauseTime;
        muzzleFlash.SetActive(true);
        Vector3 fwd = muzzlePos[randomMuzzel].TransformDirection(Vector3.forward);

        //Debug.DrawRay(muzzlePos[randomMuzzel].position, fwd * range, Color.red);

        //Laser shooting
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(muzzlePos[randomMuzzel].position, fwd, out hit, range))
        {
            //GameObject laser = GameObject.Instantiate(LaserPrefab, transform.position, transform.rotation) as GameObject;
            //laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject laser = GameObject.Instantiate(LaserPrefab, muzzlePos[randomMuzzel].position, transform.rotation) as GameObject;
            laser.GetComponent<RangeEnemyLaserBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 0.2f);


        }


        NewPlayerScript.Instance.TakeDamage(rangeEnemyDamage);
    }

    void Tracking()
    {
        Vector3 fwd = muzzlePos[randomMuzzel].TransformDirection(Vector3.forward);
        RaycastHit hit;
        //Debug.DrawRay(muzzlePos[randomMuzzel].position, fwd * range, Color.green);

        if (Physics.Raycast(muzzlePos[randomMuzzel].position, fwd, out hit, range))
        {
            if (hit.collider.CompareTag("PlayerTag"))
            {
                canSee = true;
                //OpenTurret();
            }
        }
        else
        {
            canSee = false;
            //CloseTurret();
        }
    }

    public void OpenTurret()
    {
        ani.SetBool("TurretCondition", true);
    }

    public void CloseTurret()
    {
        ani.SetBool("TurretCondition", false);
    }


}
