using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimController : MonoBehaviour
{
    [SerializeField]
    private EnemyAttackTrigger leftArm;

    [SerializeField]
    private EnemyAttackTrigger rightArm;

    public void EnemyAttackStart()
    {
        leftArm.gameObject.SetActive(true);
        rightArm.gameObject.SetActive(true);
    }

    public void EnemyAttackStop()
    {
        leftArm.gameObject.SetActive(false);
        rightArm.gameObject.SetActive(false);
    }
}
