using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Invector.CharacterController;

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

    [SerializeField]
    private float cooldown = 1.0f;
    private float waitTime;
   

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
        if (Input.GetButtonDown("Fire1") && delayAttack < Time.time && Time.timeScale != 0)
        {
            delayAttack = Time.time + 0.8f;
            waitTime = 0.0f;
            Attack();
        }

        waitTime += Time.deltaTime;

        if(waitTime > cooldown)
        {
            attackIndex = 0;
        }
    }

    void Attack()
    {
       
        isAttacking = true;
        Vector3 atkDir = mainCmra.transform.forward;
        atkDir.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(atkDir);

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
