using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingPath : MonoBehaviour
{
    public List<WanderingWaypoint> path = new List<WanderingWaypoint>();

    private void Awake()
    {
        //foreach (var point in gameObject.GetComponentsInChildren<WanderingWaypoint>())
        //{
        //    path.Add(point);
        //}
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < path.Count - 1; ++i)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
        }
    }
}
