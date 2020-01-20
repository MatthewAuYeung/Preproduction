using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public GameObject bomb;
    public Transform bombSpawnTranform;
    public float throwPower = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject newBomb = Instantiate(bomb, bombSpawnTranform.position, Quaternion.identity);
            Vector3 throwForce = bombSpawnTranform.forward * throwPower;
            newBomb.GetComponent<Rigidbody>().AddForce(throwForce);
        }
    }
}
