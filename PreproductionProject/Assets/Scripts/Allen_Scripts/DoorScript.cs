using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       


       

    }

    public void OpenDoor()
    {
        //gameObject.SetActive(false);
        animator.SetBool("DoorCondition", true);

    }

    public void CloseDoor()
    {
        //gameObject.SetActive(true);
        animator.SetBool("DoorCondition", false);

    }

    public bool IsDoorActive()
    {
        return gameObject.activeSelf;
    }
}

