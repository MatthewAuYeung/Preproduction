using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    LockOnManager lockon;

    void Start()
    {
        lockon = FindObjectOfType<LockOnManager>();
    }

    private void OnBecameVisible()
    {
        //if (!lockon.screenTargets.Contains(transform))
        //    lockon.screenTargets.Add(transform);
    }

    private void OnBecameInvisible()
    {
        //if (lockon.screenTargets.Contains(transform))
        //    lockon.screenTargets.Remove(transform);
    }
}
