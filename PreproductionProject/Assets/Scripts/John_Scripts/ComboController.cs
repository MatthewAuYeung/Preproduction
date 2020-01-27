using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidBody;

    int noOfClicks;                 // Determines which animation will play
    bool canClick;                  // Locks ability to click during animation event

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        noOfClicks = 0;             // numbers of clicks
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ComboStarter();
        }
    }

    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;
        }

        if (noOfClicks == 1)
        {
            animator.SetInteger("animation", 11);
        }
    }

    public void ComboCheck()
    {
        canClick = false;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("State_11") && noOfClicks == 1)
        {// If the first animation is still playing and only 1 click has happended, return to idle
            animator.SetInteger("animation", 1);
            canClick = true;
            noOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("State_11") && noOfClicks == 2)
        {// If the first animation is still playing and at least 2 clicks has happended, continue the combo
            animator.SetInteger("animation", 13);
            canClick = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("State_12") && noOfClicks == 2)
        {// If the second animation is still playing and only 2 clicks has happended, return to idle
            animator.SetInteger("animation", 1);
            canClick = true;
            noOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("State_12") && noOfClicks == 3)
        {// If the second animation is still playing and at least 3 clicks has happended, continue the combo
            animator.SetInteger("animation", 13);
            canClick = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("State_13") )
        {// Since this is the third and last animation, return to idle
            animator.SetInteger("animation", 1);
            canClick = true;
            noOfClicks = 0;
        }
    }
}
