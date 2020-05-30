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
    private const int totalAttacks = 6;

    float delayAttack = 0.0f;

    [SerializeField]
    private float cooldown = 1.0f;
    private float waitTime;

    private float dpadinput;
    private float lastinput;

    //=============================
    public enum comboSelection
    {
        firstAttackCombo,
        secondAttackCombo,
        testAttackCombo
    }

    public comboSelection currentCombo;

    private int startAttackIndex = 0;
    private int endAttackIndex = 0;
    private int keySelection = 0;
    //=============================


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

       

        dpadinput = Input.GetAxisRaw("DPad_LR");
        if(dpadinput != lastinput)
        {
            if (dpadinput == 1.0f)
            {
                lastinput = dpadinput;
                Debug.Log("Right");
                //=================
                keySelection++;
                if (keySelection > 1)
                {
                    keySelection = 0;
                }
                currentCombo = (comboSelection)keySelection;
                //=================
            }
            else if(dpadinput == -1.0f)
            {
                lastinput = dpadinput;
                Debug.Log("Left");
                //=================
                keySelection--;
                if (keySelection < 0)
                {
                    keySelection = 1;
                }
                currentCombo = (comboSelection)keySelection;
                //=================
            }
            else if(dpadinput == 0.0f)
            {
                lastinput = 0.0f;
            }
        }

        //============
        //choose start index and end index
        switch (currentCombo)
        {
            case comboSelection.firstAttackCombo:
                startAttackIndex = 0;
                endAttackIndex = 3;
                break;
            case comboSelection.secondAttackCombo:
                startAttackIndex = 3;
                endAttackIndex = 6;
                break;
            case comboSelection.testAttackCombo:
                startAttackIndex = 0;
                endAttackIndex = 4;
                break;
            default:
                break;
        }
        if (waitTime > cooldown)
        {
            attackIndex = startAttackIndex;
        }
        //============
    }

    void Attack()
    {
       
        isAttacking = true;
        Vector3 atkDir = mainCmra.transform.forward;
        atkDir.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(atkDir);

        string attackTrigger = "Attack" + (attackIndex+1).ToString();

        animator.SetTrigger(attackTrigger);

        if (attackIndex < endAttackIndex - 1)
            attackIndex++;
        else
            attackIndex = startAttackIndex;

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
