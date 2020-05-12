using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform.parent;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
