using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Collider weaponColldier;

    private void Start()
    {
        weaponColldier.enabled = false;
    }

    public void AttackStart()
    {
        weaponColldier.enabled = true;
    }

    public void AttackStop()
    {
        weaponColldier.enabled = false;
    }
}
