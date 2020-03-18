using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackManager : MonoBehaviour
{
    public Action OnAttackStart, OnAttackStop;
    private Camera mainCmra;

    [SerializeField]
    Collider attackCollider;
    
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
        attackCollider.gameObject.SetActive(false);

        attackIndex = 0;             // numbers of clicks
        canClick = true;
        mainCmra = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 atkDir = mainCmra.transform.forward;
        atkDir.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(atkDir);

        if (Input.GetButtonDown("Fire1") && delayAttack < Time.time && Time.timeScale != 0)
        {
            delayAttack = Time.time + 0.8f;
           
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

    void AttackStart()
    {
        attackCollider.gameObject.SetActive(true);
    }

    void AttackStop()
    {
        attackCollider.gameObject.SetActive(false);

        isAttacking = false;

        OnAttackStop?.Invoke();
    }
}
