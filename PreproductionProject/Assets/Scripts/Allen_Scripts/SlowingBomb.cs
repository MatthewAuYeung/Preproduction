using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowingBomb : MonoBehaviour
{
    public GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {
        // instantiate explosion (include bomb slowing effect
        Instantiate(explosion, transform.position, Quaternion.identity);

        // destroy bomb
        Destroy(gameObject);
    }
}
