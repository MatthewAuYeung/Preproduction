using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbilityTrigger : MonoBehaviour
{
    public GameObject AbilityIcon;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerTag"))
        {
            AbilityIcon.SetActive(true);
            NewPlayerScript.Instance.UnlockBombAbility();
            Destroy(gameObject);
        }
    }
}
