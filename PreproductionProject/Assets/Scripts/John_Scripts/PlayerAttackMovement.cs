using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool doMove = false;

    private void Awake()
    {
        GetComponent<AttackManager>().OnAttackStart += AttackStart;
        GetComponent<AttackManager>().OnAttackStop += AttackStop;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (doMove)
            transform.position = transform.position + (transform.forward * Time.fixedDeltaTime * speed);
    }

    void AttackStart()
    {
        doMove = true;   
    }

    void AttackStop()
    {
        doMove = false;
    }
}
