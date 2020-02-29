using System.Collections;
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
            playerScript.IncrementHitPoint();
        }
    }
}


