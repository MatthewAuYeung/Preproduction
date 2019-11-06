using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public List<KeyScript> Keys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Keys.RemoveAll(item => item == null);
        if (Input.GetKey(KeyCode.A) )
        {
            if (Keys.Count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

