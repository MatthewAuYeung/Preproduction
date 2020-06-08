using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimatorTest : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)) //  <---you should use your own codition
        {
            // this is how to use parameter in the animator to open door
            animator.SetBool("DoorCondition", true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            // this is how to use parameter in the animator to close door
            animator.SetBool("DoorCondition", false);
        }
    }
}
