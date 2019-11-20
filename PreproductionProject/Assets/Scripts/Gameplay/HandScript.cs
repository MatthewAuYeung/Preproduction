using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private GunScript _gun;

    private void Awake()
    {
        _gun = GetComponentInChildren<GunScript>();

    }

    public void Action()
    {
        _gun.Shoot();
    }
}
