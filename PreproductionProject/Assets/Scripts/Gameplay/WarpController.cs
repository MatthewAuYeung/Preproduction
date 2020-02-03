using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpController : MonoBehaviour
{
    [SerializeField]
    private float warpAttackDmg = 1.0f;

    [SerializeField]
    private float warpRange = 1.0f;
    [SerializeField]
    private float warpDuration = 1.0f;

    [SerializeField]
    private float warpOffset = 0.5f;

    public GameObject ybot;

    private LockOnManager lockOnManager;

    private void Awake()
    {
        lockOnManager = FindObjectOfType<LockOnManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FreeWarp();
        }

        if(lockOnManager.GetIsLockOn() && Input.GetKeyDown(KeyCode.F))
        {
            WarpAttack(lockOnManager.GetClosestEnemy());
        }
    }

    private void WarpToNewPos(Vector3 targetPos)
    {
        Vector2 PlayerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 ObjPos = new Vector2(targetPos.x, targetPos.z);
        Vector2 OffsetVec = PlayerPos - ObjPos;
        Vector2 OffsetDir = OffsetVec.normalized;
        Vector3 newWarpPos = targetPos;
        newWarpPos.x += OffsetDir.x * warpOffset;
        newWarpPos.z += OffsetDir.y * warpOffset;

        newWarpPos.y = transform.position.y;

        transform.DOMove(newWarpPos, warpDuration).OnComplete(() => EndWarp());
    }

    private void FreeWarp()
    {
        Vector3 warpDir = Camera.main.transform.forward;
        warpDir.y = 0.0f;
        RaycastHit hit;
        ShowBody(false);
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
        WarpToNewPos(targetPos);
    }

    private void EndWarp()
    {
        ShowBody(true);
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
