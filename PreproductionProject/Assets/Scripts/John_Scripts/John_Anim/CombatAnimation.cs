using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimation : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    Collider attackCollider;

    void Start()
    {
        attackCollider.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
      
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
            }
    }

    void Attacking()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()                   // change condition to 2
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("AttackCondition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("AttackCondition", 0);
        anim.SetBool("attacking", false);
    }

    void AttackStart()
    {
        attackCollider.gameObject.SetActive(true);
    }

    void AttackStop()
    {
        attackCollider.gameObject.SetActive(false);
    }
}

