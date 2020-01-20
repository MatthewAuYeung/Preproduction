using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public float attackDmg;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            BaseEnemyScript enemy = other.GetComponent<BaseEnemyScript>();
            enemy.TakeDamage(attackDmg);
        }
    }
}
