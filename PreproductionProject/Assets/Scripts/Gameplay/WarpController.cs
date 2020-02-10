using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float manaUsed;

    public GameObject ybot;

    private NewPlayerScript player;
    private LockOnManager lockOnManager;

    float currentTime;
    float warpCooldown;
    bool isWarping;

    private void Awake()
    {
        player = FindObjectOfType<NewPlayerScript>();
        warpCooldown = player.GetWarpCooldown();
        lockOnManager = FindObjectOfType<LockOnManager>();
    }

    private void Update()
    {
        if (!player.HasMana(manaUsed))
            return;

        if(!isWarping && currentTime < Time.time)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (lockOnManager.GetIsLockOn())
                {
                    WarpAttack(lockOnManager.GetClosestEnemy());
                }
                else
                    FreeWarp();
            }
        }
    }

    private void WarpToNewPos(Vector3 targetPos)
    {
        // Calculate the new position for the warp
        Vector2 PlayerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 ObjPos = new Vector2(targetPos.x, targetPos.z);
        Vector2 OffsetVec = PlayerPos - ObjPos;
        Vector2 OffsetDir = OffsetVec.normalized;
        Vector3 newWarpPos = targetPos;
        newWarpPos.x += OffsetDir.x * warpOffset;
        newWarpPos.z += OffsetDir.y * warpOffset;
        // Keeps the y position as before
        //newWarpPos.y = transform.position.y;

        transform.DOMove(newWarpPos, warpDuration).OnComplete(() => EndWarp());
    }

    private void FreeWarp()
    {
        isWarping = true;
        Vector3 warpDir = Camera.main.transform.forward;
        warpDir.y = 0.0f;
        RaycastHit hit;
        ShowBody(false);
        player.UseMana(manaUsed);

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
        transform.DOMove(transform.position + warpDir.normalized * warpRange, warpDuration).OnComplete(() => EndWarp());
    }

    private void WarpAttack(Vector3 targetPos)
    {
        ShowBody(false);
        player.UseMana(manaUsed);

        WarpToNewPos(targetPos);
    }

    private void EndWarp()
    {
        ShowBody(true);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.up, transform.forward * warpRange);
    }
}
