﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Invector.CharacterController;

public class AttackManager : MonoBehaviour
{
    public Action<float> OnAttackStart;
    public Action OnAttackStop;
    private Camera mainCmra;

    [SerializeField]
    private Collider redSwordCollider;
    [SerializeField]
    private Collider blueSwordCollider;
    [SerializeField]
    private Collider goldSwordCollider;


    private GameObject redSword;
    private GameObject blueSword;
    private GameObject goldSword;
    private GameObject currentSword;

    //[SerializeField]
    private Collider attackCollider;
    
    private  Animator animator;
    private Rigidbody rigidBody;
    public bool isAttacking;
    private bool showSword;

    private int attackIndex = 0;                 // Determines which animation will play
    private bool canClick;                  // Locks ability to click during animation event
    private const int totalAttacks = 6;

    private float delayAttack = 0.0f;
    private float swordTimer = 0.0f;

    [SerializeField]
    private float cooldown = 1.0f;
    [SerializeField]
    private float swordDisapearTime = 3.0f;

    private float waitTime;

    private float dpadinput;
    private float lastinput;

    private vThirdPersonController controller;

    [Header("Attack Distances")]
    [SerializeField]
    private float light_attack1 = 3.0f;
    [SerializeField]
    private float light_attack2 = 3.0f;
    [SerializeField]
    private float light_attack3 = 3.0f;
    [SerializeField]
    private float light_attack4 = 3.0f;

    [SerializeField]
    private float heavy_attack1 = 3.0f;
    [SerializeField]
    private float heavy_attack2 = 3.0f;
    [SerializeField]
    private float heavy_attack3 = 3.0f;
    [SerializeField]
    private float heavy_attack4 = 3.0f;
    [SerializeField]
    private float heavy_attack5 = 3.0f;
    //=============================
    public enum comboSelection
    {
        lightStance,
        heavyStacne,
        testLightAttackCombo,
        testHeavyAttackCombo
    }

    public comboSelection currentCombo;

    private int startAttackIndex = 0;
    private int endAttackIndex = 0;
    private int keySelection = 0;
    //=============================

    public enum SwordType
    {
        RedSword,
        BlueSword,
        GoldSword,
        MAX_SWORDTYPE
    }
    private int swordIndex;
    public SwordType currentSwordType;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        redSword = redSwordCollider.transform.parent.gameObject;
        blueSword = blueSwordCollider.transform.parent.gameObject;
        goldSword = goldSwordCollider.transform.parent.gameObject;

        redSword.gameObject.SetActive(false);
        blueSword.gameObject.SetActive(false);
        goldSword.gameObject.SetActive(false);

        redSwordCollider.gameObject.SetActive(false);
        blueSwordCollider.gameObject.SetActive(false);
        goldSwordCollider.gameObject.SetActive(false);

        CheckCurrentSword();

        currentSword.SetActive(true);
        attackCollider.gameObject.SetActive(false);

        attackIndex = 0;             // numbers of clicks
        canClick = true;
        mainCmra = Camera.main;
        SetCombo(currentCombo);
        swordIndex = (int)currentSwordType;
        controller = GetComponent<vThirdPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (swordIndex < (int)SwordType.MAX_SWORDTYPE - 1)
                swordIndex++;
            else
                swordIndex = 0;

            currentSwordType = (SwordType)swordIndex;
        }

        CheckCurrentSword();

        if (Input.GetKeyDown(KeyCode.U))
        {
            controller.SetCanMove(false);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            controller.SetCanMove(true);
        }
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

        SetCombo(currentCombo);
        //============
        //choose start index and end index
        if (waitTime > cooldown)
        {
            attackIndex = startAttackIndex;
        }
        //============

        if (showSword)
        {
            currentSword.SetActive(true);
        }
        else
        {
            currentSword.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1") && delayAttack < Time.time && Time.timeScale != 0)
        {
            delayAttack = Time.time + 0.8f;
            waitTime = 0.0f;
            swordTimer = Time.time + swordDisapearTime;
            showSword = true;
            Attack();
        }

        waitTime += Time.deltaTime;
        if (swordTimer < Time.time)
        {
            // play particle
            showSword = false;
        }
    }

    private void SetCombo(comboSelection combo)
    {
        switch (combo)
        {
            case comboSelection.lightStance:
                startAttackIndex = 0;
                endAttackIndex = 4;
                break;
            case comboSelection.heavyStacne:
                startAttackIndex = 4;
                endAttackIndex = 9;
                break;
            case comboSelection.testLightAttackCombo:
                startAttackIndex = 0;
                endAttackIndex = 4;
                break;
            case comboSelection.testHeavyAttackCombo:
                startAttackIndex = 4;
                endAttackIndex = 8;
                break;
            default:
                break;
        }
    }
    void Attack()
    {
        controller.SetCanMove(false);
        isAttacking = true;
        Vector3 atkDir = mainCmra.transform.forward;
        atkDir.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(atkDir);

        string attackTrigger = "Attack" + (attackIndex+1).ToString();

        animator.SetTrigger(attackTrigger);

        OnAttackStart?.Invoke(GetAttackDistance(attackIndex));

        if (attackIndex < endAttackIndex - 1)
            attackIndex++;
        else
            attackIndex = startAttackIndex;
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

    private float GetAttackDistance(int index)
    {
        float result;
        switch (index)
        {
            case 0:
                result = light_attack1;
                break;
            case 1:
                result = light_attack2;
                break;
            case 2:
                result = light_attack3;
                break;
            case 3:
                result = light_attack4;
                break;
            case 4:
                result = heavy_attack1;
                break;
            case 5:
                result = heavy_attack2;
                break;
            case 6:
                result = heavy_attack3;
                break;
            case 7:
                result = heavy_attack4;
                break;
            case 8:
                result = heavy_attack5;
                break;

            default:
                result = 3.0f;
                break;
        }
        return result;
    }

    private void CheckCurrentSword()
    {
        switch (currentSwordType)
        {
            case SwordType.RedSword:
                SwordSetActive(SwordType.RedSword, true);
                SwordSetActive(SwordType.BlueSword, false);
                SwordSetActive(SwordType.GoldSword, false);

                currentSword = redSword;
                attackCollider = redSwordCollider;
                break;
            case SwordType.BlueSword:
                SwordSetActive(SwordType.RedSword, false);
                SwordSetActive(SwordType.BlueSword, true);
                SwordSetActive(SwordType.GoldSword, false);

                currentSword = blueSword;
                attackCollider = blueSwordCollider;
                break;
            case SwordType.GoldSword:
                SwordSetActive(SwordType.RedSword, false);
                SwordSetActive(SwordType.BlueSword, false);
                SwordSetActive(SwordType.GoldSword, true);

                currentSword = goldSword;
                attackCollider = goldSwordCollider;
                break;
            case SwordType.MAX_SWORDTYPE:
                SwordSetActive(SwordType.RedSword, true);
                SwordSetActive(SwordType.BlueSword, false);
                SwordSetActive(SwordType.GoldSword, false);

                currentSword = redSword;
                attackCollider = redSwordCollider;
                break;
        }
    }

    private void SwordSetActive(SwordType sword, bool state)
    {
        switch (sword)
        {
            case SwordType.RedSword:
                redSword.SetActive(state);
                //redSwordCollider.gameObject.SetActive(state);
                break;
            case SwordType.BlueSword:
                blueSword.SetActive(state);
                //blueSwordCollider.gameObject.SetActive(state);
                break;
            case SwordType.GoldSword:
                goldSword.SetActive(state);
                //goldSwordCollider.gameObject.SetActive(state);
                break;
            case SwordType.MAX_SWORDTYPE:
                redSword.SetActive(state);
                //redSwordCollider.gameObject.SetActive(state);
                break;
        }
    }
}
