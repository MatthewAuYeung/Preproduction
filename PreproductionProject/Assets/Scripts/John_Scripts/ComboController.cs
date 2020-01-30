using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComboController : MonoBehaviour
{
    public Action OnAttackStart, OnAttackStop;

    Animator animator;
    Rigidbody rigidBody;
    bool isAttacking;

    int attackIndex = 0;                 // Determines which animation will play
    bool canClick;                  // Locks ability to click during animation event
    private const int totalAttacks = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        attackIndex = 0;             // numbers of clicks
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        string attackTrigger = "Attack" + (attackIndex+1).ToString();

        animator.SetTrigger(attackTrigger);

        if (attackIndex < totalAttacks-1)
            attackIndex++;
        else
            attackIndex = 0;

        OnAttackStart?.Invoke();
    }

    void StopAttack()
    {
        isAttacking = false;

        OnAttackStop?.Invoke();
    }
}
