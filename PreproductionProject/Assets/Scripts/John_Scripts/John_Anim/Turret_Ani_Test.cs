using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Ani_Test : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))//  <---you should use your own codition
        {
            // this is how to use parameter in the animator to open the fking egg
            animator.SetBool("TurretCondition", true);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            // this is how to use parameter in the animator to close it
            animator.SetBool("TurretCondition", false);
        }
    }
}
