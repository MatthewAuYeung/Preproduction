using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform target;
    public List<EnemyScript> enemies;
    private bool IsAllEnemiesKilled = false;
    private void Awake()
    {
        
    }
    private void Start()
    {
        enemies = new List<EnemyScript>();
        GetComponentsInChildren<EnemyScript>(false,enemies);
    }
    private void Update()
    {
        Debug.Log(enemies.Count.ToString());
        if (enemies.Count == 0)
        {
            IsAllEnemiesKilled = true;
        }
    }

    public bool CheckBattleDone()
    {
        return IsAllEnemiesKilled;
    }

    public void SetBattleDone(bool state)
    {
        IsAllEnemiesKilled = state;
    }
}
