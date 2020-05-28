using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyController : MonoBehaviour
{
    public Transform target, aim, head;
    public float reloadTime = 1.0f;
    public float turnSpeed = 5.0f;
    public float firePauseTime = 0.25f;
    public float range = 5.0f;
    public float damage = 10.0f;
    public Transform[] muzzlePos;
    public bool canSee = false;
    public GameObject muzzleFlash;

    public float nextFireTime;
    private float nextMoveTime;
    public int randomMuzzel;

    void Start()
    {
        muzzleFlash.SetActive(false);
    }

    void Update()
    {
        Tracking();
        AimFire();
    }

    void AimFire()
    {
        if (target)
        {
            if (Time.time >= nextMoveTime)
            {
                aim.LookAt(target);
                aim.eulerAngles = new Vector3(0, aim.eulerAngles.y, 0);
                head.rotation = Quaternion.Lerp(head.rotation, aim.rotation, Time.deltaTime * turnSpeed);
            }

            if (/*Time.time >= nextFireTime && */canSee)
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
    }

    void Tracking()
    {
        Vector3 fwd = muzzlePos[randomMuzzel].TransformDirection(Vector3.forward);
        //Vector3 fwd = muzzlePos[randomMuzzel].transform.forward;
        RaycastHit hit;
        Debug.DrawRay(muzzlePos[randomMuzzel].position, fwd * range, Color.green);

        if (Physics.Raycast(muzzlePos[randomMuzzel].position, fwd, out hit, range))
        {
            if (hit.collider.CompareTag("PlayerTag"))
            {
                canSee = true;
                GetComponentInParent<NewPlayerScript>().TakeDamage(damage);
            }
        }
        else
        {
            canSee = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!target)
        {
            if (col.CompareTag("PlayerTag"))
            {
                nextFireTime = Time.time + (reloadTime * 0.5f);
                target = col.gameObject.transform;
            }
        }
            
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.transform == target)
            target = null;
    }

}
