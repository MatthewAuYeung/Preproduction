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

    float delayAttack = 0.0f;

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
        if(Input.GetMouseButtonDown(0) && delayAttack < Time.time)
        {
            delayAttack = Time.time + 0.8f;
            Attack();
        }
        
        if (!animator.GetCurrentAnimatorStateInfo(1).IsName("NotAttacking") /*&& animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f*/)
        {
            animator.SetFloat("InputVertical", 0f);//, 0.1f, Time.deltaTime);
            animator.SetFloat("InputHorizontal", 0f);//, 0.1f, Time.deltaTime);
            Debug.Log("Speed 0");
        }
        else if(animator.GetCurrentAnimatorStateInfo(1).IsName("NotAttacking"))
        {
            StopAttack();
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
