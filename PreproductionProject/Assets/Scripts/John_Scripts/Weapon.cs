﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    NewPlayerScript playerScript;
    private float attackDmg;
    
    private void Awake()
    {
        playerScript = FindObjectOfType<NewPlayerScript>();
        attackDmg = playerScript.AttckDamage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            enemy.TakeDamage(attackDmg);
            RaycastHit hit;
            if(Physics.Raycast(transform.position + (transform.forward * 1f), - transform.forward, out hit))
            {
                enemy.KnockBack(5.0f, hit.point);
            }
            playerScript.IncrementHitPoint();
        }
    }
}


