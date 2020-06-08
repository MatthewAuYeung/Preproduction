﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPrefabScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float Range;
    public List<KeyScript> Keys;
    public GameObject enemyListHolder;

    private DoorScript _door;
    private GameObject _player;
    private float _distanceToPlayer;
    private List<EnemyScript>  enemies = new List<EnemyScript>();


    private void Awake()
    {
        _door = GetComponentInChildren<DoorScript>();
        _player = GameObject.FindGameObjectWithTag("PlayerTag");
    }
    void Start()
    {
        foreach (var enemy in enemyListHolder.GetComponentsInChildren<EnemyScript>())
        {
            enemies.Add(enemy);
            enemy.OnDeath += (x) => { enemies.Remove(x); };
        }
    }

    // Update is called once per frame
    void Update()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        Keys.RemoveAll(item => item == null);

        if (enemies.Count == 0)
            _door.OpenDoor();

        if(_distanceToPlayer <= Range)
        {
            if (Input.GetButtonDown("Action"))
            {
                if (_player.GetComponentInParent<NewPlayerScript>().playerKeyCount > 0) 
                {
                    _door.OpenDoor();
                    _player.GetComponentInParent<NewPlayerScript>().RemoveKey();
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.J)) //  <---you should use your own codition
        {
            // this is how to use parameter in the animator to open door
            _door.OpenDoor();
        }
        //if (!_door.IsDoorActive())
        //{
        //    if (Input.GetButtonDown("Action"))
        //    {
        //        _door.CloseDoor();
        //    }
        //}
    }

    public void CloseDoor()
    {
        _door.CloseDoor();
    }

    public void RemoveEnemy(EnemyScript enemy)
    {
        enemies.Remove(enemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
