using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbilityTrigger : MonoBehaviour
{
    [SerializeField]
    private NewPlayerScript.AbilityType ability;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerTag"))
        {
            switch (ability)
            {
                case NewPlayerScript.AbilityType.Warp:
                    break;
                case NewPlayerScript.AbilityType.Bomb:
                    NewPlayerScript.Instance.UnlockBombAbility();
                    break;
                case NewPlayerScript.AbilityType.PhaseGrab:
                    NewPlayerScript.Instance.UnlockPhaseGrab();
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
