using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTrigger : MonoBehaviour
{
    private RangeEnemyController parent;
    private void Awake()
    {
        parent = GetComponentInParent<RangeEnemyController>();
    }

    void OnTriggerStay(Collider col)
    {
        if (!parent.target)
        {
            if (col.CompareTag("PlayerTag"))
            {
                var nextFireTime = parent.GetNextFireTime();
                nextFireTime = Time.time + (parent.reloadTime * 0.5f);
                parent.target = col.gameObject.transform;
                parent.OpenTurret();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.transform == parent.target)
        {
            parent.target = null;
            parent.CloseTurret();

        }
    }
}
