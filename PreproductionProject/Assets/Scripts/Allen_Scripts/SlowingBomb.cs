using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowingBomb : MonoBehaviour
{
    public GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlatformTag"))
        {
            MovingPlatform platform = collision.gameObject.GetComponentInParent<MovingPlatform>();
            platform.speed *= 0.5f;
        }

        // instantiate explosion (include bomb slowing effect
        Instantiate(explosion, transform.position, Quaternion.identity);

        // destroy bomb
        Destroy(gameObject);

    }
}
