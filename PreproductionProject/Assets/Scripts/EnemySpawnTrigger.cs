using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyList;

    private List<BaseEnemyScript> enemies = new List<BaseEnemyScript>();
    private bool isTriggered = false;

    private void Awake()
    {
        foreach (var enemy in EnemyList.GetComponentsInChildren<BaseEnemyScript>())
        {
            enemies.Add(enemy);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered)
            return;
        if(other.CompareTag("PlayerTag"))
        {
            if (enemies.Count > 0)
            {
                foreach (var enemy in enemies)
                {
                    enemy.SpawnEnemy();
                }
            }
            isTriggered = true;
        }
    }
}
