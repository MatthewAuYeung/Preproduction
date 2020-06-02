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
            BaseEnemyScript enemy = other.GetComponent<BaseEnemyScript>();
            enemy.TakeDamage(attackDmg);
            enemy.ChangeState(BaseEnemyScript.EnemyState.Damaged);
            RaycastHit hit;
            if(Physics.Raycast(transform.position + (transform.forward * 1f), - transform.forward, out hit))
            {
                enemy.KnockBack(5.0f, hit.point);
            }
            playerScript.IncrementHitPoint();
        }
    }
}


