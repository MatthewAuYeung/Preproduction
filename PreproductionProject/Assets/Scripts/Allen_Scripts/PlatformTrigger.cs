using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public MovingPlatform platform;
    private void OnTriggerEnter(Collider other)
    {
        platform.NextPlatform();
    }
}
