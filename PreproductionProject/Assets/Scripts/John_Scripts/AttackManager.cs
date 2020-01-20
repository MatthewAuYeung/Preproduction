using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Camera cam;
    public WeaponManager myWeapon;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void DoAttack()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, myWeapon.attackDmg))
        {
            if(hit.collider.tag == "Enemy")
            {
                BaseEnemyScript health = hit.collider.GetComponent<BaseEnemyScript>();
                //health.TakeDmg(myWeapon.attackDmg);
            }
        }
    }
}
