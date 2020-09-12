using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowingBomb : MonoBehaviour
{
    public GameObject explosion;
    public GameObject particleAttractor;
    public float range = 10.0f;
    public Vector3 startingPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlatformTag"))
        {
            MovingPlatform platform = other.gameObject.GetComponentInParent<MovingPlatform>();
            //platform.speed *= 0.5f;
            platform.SlowFromBomb();
        }
        // instantiate explosion (include bomb slowing effect
        Instantiate(explosion, transform.position, Quaternion.identity);

        // destroy bomb
        DestroyOnHit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlatformTag"))
        {
            MovingPlatform platform = collision.gameObject.GetComponentInParent<MovingPlatform>();
            //platform.speed *= 0.5f;
            platform.SlowFromBomb();
        }
        // instantiate explosion (include bomb slowing effect
        Instantiate(explosion, transform.position, Quaternion.identity);

        // destroy bomb
        DestroyOnHit();
    }


    private void Update()
    {
        if (Vector3.Distance(startingPosition,transform.position) > range)
        {
            Destroy(particleAttractor.transform.parent.gameObject);
            Destroy(gameObject);
        }
        particleAttractor.transform.position = transform.position;
    }

    public void DestroyOnHit()
    {
        Destroy(particleAttractor.transform.parent.gameObject);
        Destroy(gameObject);
    }

}
