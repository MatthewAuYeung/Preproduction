using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPrefabScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<KeyScript> Keys;
    private DoorScript _door;
    private void Awake()
    {
        _door = GetComponentInChildren<DoorScript>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Keys.RemoveAll(item => item == null);
        if (Input.GetKey(KeyCode.A))
        {
            if (Keys.Count == 0)
            {
                _door.OpenDoor();

            }
        }
        if (!_door.IsDoorActive())
        {
            if (Input.GetKey(KeyCode.D))
            {
                _door.CloseDoor();
            }
        }

    }
}
