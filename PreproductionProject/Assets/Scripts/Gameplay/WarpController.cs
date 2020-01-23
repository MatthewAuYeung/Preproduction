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

    public GameObject ybot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 warpDir = Camera.main.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, warpDir.normalized, out hit, warpRange))
            {
                if (hit.collider.gameObject.CompareTag("NotWarpable"))
                {
                    Debug.Log(hit.transform.name); ;
                }
            }
        }

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
        if (Physics.Raycast(transform.position + transform.up, warpDir.normalized, out hit, warpRange))
        {
            var hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("NotWarpable"))
            {
                //hitObj.transform.position
                transform.DOMove(hitObj.transform.position, warpDuration).OnComplete(() => EndWarp());
            }
        }
        ShowBody(false);
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
