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
        if (!lockon.targets.Contains(transform))
            lockon.targets.Add(transform);
    }

    private void OnBecameInvisible()
    {
        if (lockon.targets.Contains(transform))
            lockon.targets.Remove(transform);
    }
}
