using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform guntip;

    [Header("Attributes")]
    public float damage = 10.0f;
    public float range = 100.0f;
    public float cooldown = 1.0f;

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(guntip.position, guntip.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
