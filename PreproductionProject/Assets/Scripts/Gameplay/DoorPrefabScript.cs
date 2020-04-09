using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPrefabScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float Range;
    public List<KeyScript> Keys;

    private DoorScript _door;
    private GameObject _player;
    private float _distanceToPlayer;
    private void Awake()
    {
        _door = GetComponentInChildren<DoorScript>();
        _player = GameObject.FindGameObjectWithTag("PlayerTag");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        Keys.RemoveAll(item => item == null);

        if(_distanceToPlayer <= Range)
        {
            if (Input.GetButtonDown("Action"))
            {
                if (Keys.Count == 0)
                {
                    _door.OpenDoor();

                }
            }
        }
        //if (!_door.IsDoorActive())
        //{
        //    if (Input.GetButtonDown("Action"))
        //    {
        //        _door.CloseDoor();
        //    }
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
