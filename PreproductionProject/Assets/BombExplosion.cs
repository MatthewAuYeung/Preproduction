using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombExplosion : MonoBehaviour
{
    public float enemyAgentSlowSpeed = 0.5f;
    public float effectDuration = 1f;
    public float lifeSpan = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            EnemyScript enemy = other.GetComponentInParent<EnemyScript>();
            enemy.SlowFromBomb(enemyAgentSlowSpeed, effectDuration);
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
