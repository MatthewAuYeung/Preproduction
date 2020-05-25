using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbilityTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerTag"))
        {
            NewPlayerScript.Instance.UnlockBombAbility();
            Destroy(gameObject);
        }
    }
}
