using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGettingHitAnim : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayerHurtAnimation()
    {
        animator.SetTrigger("EnemyGettingHit");
    }
}
