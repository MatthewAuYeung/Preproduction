using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Invector.CharacterController;

public class WarpController : MonoBehaviour
{
    [SerializeField]
    private float warpAttackDmg = 1.0f;

    [SerializeField]
    float warpRange = 1.0f;

    [SerializeField]
    float warpDuration = 1.0f;

    [SerializeField]
    float warpOffset = 0.5f;

    [SerializeField]
    float warpEnemyRange = 10.0f;

    [SerializeField]
    float warpEnemyDuration = 0.5f;

    [SerializeField]
    private float fresnelDurration = 5.0f;

    [SerializeField]
    bool isDebuging = true;

    [SerializeField]
    ParticleSystem blueTrail;

    [SerializeField]
    ParticleSystem whiteTrail;

    [SerializeField]
    private Material PG_glowMat;

    [SerializeField]
    private Material Warp_glowMat;

    [SerializeField]
    private Transform sword;

    [SerializeField]
    private Transform swordHand;

    [SerializeField]
    private GameObject indicator;

    public GameObject ybot;

    private NewPlayerScript player;
    private LockOnManager lockOnManager;
    private AttackManager attackManager;
    private Animator animator;
    float currentTime;
    float warpCooldown;
    bool isWarping;

    private GameObject selectedObj;
    private GameObject cloneObj;
    private bool isSelected;
    private float magnitudeBWTEnemy;
    private Rigidbody _rb;
    private Camera mainCamera;
    private float abilityDuration;
    private float abilityWaitTime;
    private Vector3 originalPos;

    private Vector3 swordOrigPos;
    private Vector3 swordOrigRot;
    private Vector3 swordOrigScale;
    private MeshRenderer swordMesh;

    private void Awake()
    {
        player = GetComponentInParent<NewPlayerScript>();
        if(player != null)
        {
            warpCooldown = player.GetWarpCooldown();
            abilityDuration = player.GetWarpEnemyDuration();
        }
        lockOnManager = GetComponent<LockOnManager>();
        attackManager = GetComponent<AttackManager>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        
        indicator.SetActive(false);
        swordOrigPos = sword.localPosition;
        swordOrigRot = sword.localEulerAngles;

    }

    private void Update()
    {
        //if (!player.HasMana(manaUsed))
        //    return;

        abilityWaitTime += Time.deltaTime;

        if(!isWarping && currentTime < Time.time)
        {
            if (Input.GetButtonDown("Warp"))
            {
                player.AbilityUsed(NewPlayerScript.AbilityType.Warp);
                //if (lockOnManager.GetIsLockOn())
                //{
                //    WarpAttack(lockOnManager.GetClosestObject());
                //}
                //else
                //    FreeWarp();
                attackManager.ShowSword();
                animator.SetTrigger("Warp");
            }
        }

        if (Input.GetButtonDown("WarpEnemy"))
        {
            if (!player.GetPhaseGrabState())
                return;

            if (!player.DoneCooldown(NewPlayerScript.AbilityType.PhaseGrab))
                return;

            // Warping the enemy to a new pos
            if (isSelected)
            {
                //Destroy(cloneObj);
                indicator.SetActive(false);
                WarpEnemy(selectedObj);
                isSelected = false;
                lockOnManager.SetIsSelected(isSelected);
                player.AbilityUsed(NewPlayerScript.AbilityType.PhaseGrab);
            }

            // Selecting an enemy ready to warp it to a new pos
            if (lockOnManager.GetIsLockOn() && !isSelected)
            {
                selectedObj = lockOnManager.GetClosestObject().transform.parent.gameObject;
                if(selectedObj.CompareTag("EnemyTag") || selectedObj.CompareTag("Grabable"))
                {
                    // Start the timer to for ability duration
                    abilityWaitTime = 0.0f;
                    // Record the original pos of the selected enemy
                    originalPos = selectedObj.transform.position;
                    // Calculate the vector the player and the enemy
                    Vector3 temp = selectedObj.transform.position - transform.position;
                    magnitudeBWTEnemy = temp.magnitude;
                    // Make the enemy object disappear
                    selectedObj.SetActive(false);
                    isSelected = true;
                    // Tell the LockOnManager that the player is using the ability
                    lockOnManager.SetIsSelected(isSelected);

                    #region OldStuff
                    //// Create a clone obj for the indicator
                    //cloneObj = Instantiate(selectedObj);
                    //// Delete everything beside the mesh

                    //// Change the material to a transparent texture
                    ////var meshRenderer = cloneObj.GetComponent<MeshRenderer>();
                    ////meshRenderer.material = glowMat;
                    //if (selectedObj.CompareTag("EnemyTag"))
                    //{
                    //    SkinnedMeshRenderer[] skinMeshList = cloneObj.GetComponentsInChildren<SkinnedMeshRenderer>();
                    //    foreach (SkinnedMeshRenderer smr in skinMeshList)
                    //    {
                    //        smr.material = glowMat;
                    //    }
                    //}
                    //else
                    //{
                    //    MeshRenderer meshRenderer = cloneObj.GetComponentInChildren<MeshRenderer>();
                    //    meshRenderer.material = glowMat;
                    //    Destroy(cloneObj.GetComponentInChildren<Rigidbody>());
                    //}

                    //cloneObj.SetActive(false);
                    #endregion

                    lockOnManager.TurnoffLockOn();
                }
            }
        }

        // If the player hasn't warp the enemy to a new pos in a speacific time,
        // the enemy remains at his original pos then start the cooldown for the ability
        if(isSelected)
        {
            if(abilityWaitTime > abilityDuration)
            {
                //Destroy(cloneObj);
                indicator.SetActive(false);
                selectedObj.transform.position = originalPos;
                selectedObj.SetActive(true);
                isSelected = false;
                lockOnManager.SetIsSelected(isSelected);
                player.AbilityUsed(NewPlayerScript.AbilityType.PhaseGrab);
            }
        }

        // Showing the indicator for the enemy's position when using the ability
        if (isSelected)
        {
            Vector3 targetPos = indicator.transform.position;
            Vector3 warpDir = mainCamera.transform.forward;
            RaycastHit hit;
            Vector3 newPos;
            if (Physics.Raycast(mainCamera.transform.position, warpDir, out hit, warpEnemyRange))
            {
                newPos = hit.point;
                newPos -= warpDir.normalized * 0.75f;
            }
            else
            {
                newPos = mainCamera.transform.position + warpDir.normalized * warpEnemyRange;
            }
            indicator.transform.position = new Vector3(newPos.x, gameObject.transform.position.y, newPos.z);
            if (!indicator.activeSelf)
            {
                indicator.SetActive(true);
            }
        }
    }

    public void Warp()
    {
        if (lockOnManager.GetIsLockOn())
        {
            WarpAttack(lockOnManager.GetClosestObject());
        }
        else
            FreeWarp();
    }

    private void Fresnel()
    {
        GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
        Destroy(clone.GetComponent<WarpController>());
        Destroy(clone.GetComponent<LockOnManager>());
        var temp = clone.GetComponent<AttackManager>();
        temp.DestroySword();
        Destroy(temp);
        Destroy(clone.GetComponent<PlayerAttackMovement>());
        Destroy(clone.GetComponent<Animator>());
        Destroy(clone.GetComponent<vThirdPersonController>());
        Destroy(clone.GetComponent<vThirdPersonInput>());
        Destroy(clone.GetComponent<BombThrower>());
        Destroy(clone.GetComponent<PlayerGettingHitAnim>());
        Destroy(clone.GetComponent<Rigidbody>());

        SkinnedMeshRenderer[] skinMeshList = clone.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer smr in skinMeshList)
        {
            smr.material = Warp_glowMat;
            smr.material.DOFloat(2, "_AlphaThreshold", fresnelDurration).OnComplete(() => Destroy(clone));
        }
    }

    private void WarpToNewPos(Vector3 targetPos, GameObject target = null)
    {
        Fresnel();
        // Calculate the new position for the warp
        Vector2 PlayerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 ObjPos = new Vector2(targetPos.x, targetPos.z);
        Vector2 OffsetVec = PlayerPos - ObjPos;
        Vector2 OffsetDir = OffsetVec.normalized;
        Vector3 newWarpPos = targetPos;
        newWarpPos.x += OffsetDir.x * warpOffset;
        newWarpPos.z += OffsetDir.y * warpOffset;
        // Keeps the y position as before
        if (!lockOnManager.GetIsLockOn())
        {
            newWarpPos.y = transform.position.y;
        }
        if(target != null)
        {
            //transform.DOLookAt(targetPos, 0.2f, AxisConstraint.None);
            transform.DOMove(newWarpPos, warpDuration).OnComplete(() => EndWarp(target));
        }
        else
            transform.DOMove(newWarpPos, warpDuration).OnComplete(() => EndWarp());

        PlayParticles();
    }

    private void FreeWarp()
    {
        isWarping = true;
        Vector3 warpDir = mainCamera.transform.forward;
        warpDir.y = 0.0f;
        RaycastHit hit;

        Fresnel();

        ShowBody(false);
        transform.rotation = Quaternion.LookRotation(warpDir);


        // Raycast from the player model to check if there is a not warpable object inside the warp range
        if (Physics.Raycast(transform.position + transform.up, warpDir.normalized, out hit, warpRange))
        {
            var hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("NotWarpable"))
            {
                WarpToNewPos(hitObj.transform.position);
                return;
            }
        }
        transform.DOMove(transform.position + warpDir.normalized * warpRange, warpEnemyDuration).OnComplete(() => EndWarp());

        sword.parent = null;
        sword.DOMove(transform.position + warpDir.normalized * warpRange, warpEnemyDuration / 1.2f);
        sword.rotation = Quaternion.LookRotation(-warpDir);
        PlayParticles();
    }

    private void WarpAttack(GameObject target)
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        ShowBody(false);
        //player.UseMana(manaUsed);
        if(target.CompareTag("EnemyTag"))
        {
            var enemy = target.GetComponent<EnemyScript>();
            enemy.beingWarpAttacked = true;
            if(isDebuging)
                _rb.isKinematic = true;
            WarpToNewPos(target.transform.position, target);
        }
        WarpToNewPos(target.transform.position);
    }

    void PlayParticles()
    {
        blueTrail.Play();
        whiteTrail.Play();
    }

    IEnumerator StopParticles()
    {
        yield return new WaitForSeconds(0.5f);
        blueTrail.Stop();
        whiteTrail.Stop();
    }

    private void EndWarp(GameObject target = null)
    {
        sword.parent = swordHand;
        sword.localPosition = swordOrigPos;
        sword.localEulerAngles = swordOrigRot;

        if(target != null)
        {
            EnemyScript enemy = target.GetComponent<EnemyScript>();
            enemy.TakeDamage(30.0f);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit))
            {
                enemy.KnockBack(10.0f, hit.point);
                enemy.beingWarpAttacked = false;
            }
        }

        var rot = transform.eulerAngles;
        rot.x = 0.0f;
        transform.eulerAngles = rot;
        _rb.velocity = new Vector3();
        if(isDebuging)
            _rb.isKinematic = false;
        ShowBody(true);
        StartCoroutine(StopParticles());

        // Set a cool down for warping
        currentTime = Time.time + warpCooldown;
        isWarping = false;
    }

    void ShowBody(bool state)
    {
        SkinnedMeshRenderer[] skinMeshList = ybot.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer smr in skinMeshList)
        {
            smr.enabled = state;
        }
    }

    void WarpEnemy(GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 warpDir = mainCamera.transform.forward;
        RaycastHit hit;
        Vector3 newPos;
        if (Physics.Raycast(mainCamera.transform.position, warpDir, out hit, warpEnemyRange))
        {
            newPos = hit.point;
            if (hit.point.y < targetPos.y)
            {
                newPos.y = targetPos.y;
            }
            newPos -= warpDir.normalized * 0.75f;
        }
        else
        {
            newPos = mainCamera.transform.position + warpDir.normalized * warpEnemyRange;
        }
        target.transform.DOMove(newPos, warpDuration).OnComplete(() => EndWarpEnemy(target));
    }

    void EndWarpEnemy(GameObject target)
    {
        target.SetActive(true);
    }

    public bool IsWarping()
    {
        return isWarping;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.up, transform.forward * warpRange);
    }
}
