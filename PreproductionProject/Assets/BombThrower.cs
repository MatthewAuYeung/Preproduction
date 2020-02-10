using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public GameObject bomb;
    public Transform bombSpawnTranform;
    public float throwPower = 100f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("Throw");
            Vector3 throwDirection = Camera.main.transform.forward;
            Vector3 spawnPosition = bombSpawnTranform.position + (throwDirection * 1f);
            GameObject newBomb = Instantiate(bomb, spawnPosition, Quaternion.identity);
            Vector3 throwForce = throwDirection * throwPower;
            newBomb.GetComponent<Rigidbody>().AddForce(throwForce);
        }
    }
}
