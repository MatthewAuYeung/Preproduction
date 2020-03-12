using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyType;
    public float spawnDelay = 3.0f;
    public float startDelay = 2.0f;
    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = startDelay;
    }
    void Update()
    {
        if (nextSpawnTime < Time.time)
        {
            Instantiate(EnemyType, transform);
            nextSpawnTime += spawnDelay;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.0f, 1.0f, 1.0f));
    }
}
