using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpController : MonoBehaviour
{
    [SerializeField]
    private float warpRange = 1.0f;
    [SerializeField]
    private float warpDuration = 1.0f;

    [SerializeField]
    private float warpOffset = 0.5f;

    public GameObject ybot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FreeWarp();
        }
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
                Vector2 PlayerPos = new Vector2(transform.position.x, transform.position.z);
                Vector2 ObjPos = new Vector2(hitObj.transform.position.x, hitObj.transform.position.z);
                Vector2 OffsetVec = PlayerPos - ObjPos;
                Vector2 OffsetDir = OffsetVec.normalized;
                Vector3 newWarpPos = hitObj.transform.position;
                newWarpPos.x += OffsetDir.x * warpOffset;
                newWarpPos.z += OffsetDir.y * warpOffset;

                newWarpPos.y = transform.position.y;
                
                transform.DOMove(newWarpPos, warpDuration).OnComplete(() => EndWarp());
                return;
            }
        }
        transform.DOMove(transform.position + warpDir.normalized * warpRange, warpDuration).OnComplete(() => EndWarp());
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
