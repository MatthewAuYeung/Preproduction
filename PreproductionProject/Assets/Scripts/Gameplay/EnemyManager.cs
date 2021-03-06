﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform target;
    public List<EnemyScript> enemies;
    private bool IsAllEnemiesKilled = false;

    private void Awake()
    {
        enemies = new List<EnemyScript>(FindObjectsOfType<EnemyScript>());
    }

    private void Start()
    {

    }

    private void Update()
    {
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
