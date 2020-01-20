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
        if(Input.GetKeyDown(KeyCode.T))
        {
            FreeWarp();
        }
    }

    private void FreeWarp()
    {
        ShowBody(false);
        Vector3 warpDir = Camera.main.transform.forward;
        warpDir.y = 0.0f;
        transform.DOMove(transform.position + warpDir.normalized * warpRange, warpDuration).OnComplete(()=>EndWarp());
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
