using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
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
            BaseEnemyScript enemy = other.GetComponent<BaseEnemyScript>();
            enemy.TakeDamage(attackDmg);
            playerScript.IncrementHitPoint();
        }
    }
}


