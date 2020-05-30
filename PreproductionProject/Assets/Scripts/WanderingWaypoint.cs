using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingWaypoint : MonoBehaviour
{
    [SerializeField]
    private float Min;

    [SerializeField]
    private float Max;

    private float waitTime;

    private void Awake()
    {
        waitTime = Random.Range(Min, Max);
    }

    public float GetWaitTime()
    {
        return waitTime;
    }
}
