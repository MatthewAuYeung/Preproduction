using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGettingHitAnim : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayerHurtAnimation()
    {
        animator.SetTrigger("GettingHit");

    }
}
