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

    // Update is called once per frame
    void FixedUpdate()
    {
       if (doMove)
            transform.position = transform.position + (transform.forward * Time.fixedDeltaTime * speed);
    }

    void AttackStart(float dis)
    {
        doMove = true;
        speed = dis;
    }

    void AttackStop()
    {
        doMove = false;
    }
}
