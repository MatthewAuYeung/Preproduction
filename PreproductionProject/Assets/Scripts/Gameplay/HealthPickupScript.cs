using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    public float heal = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with " + other.gameObject.name);
        if (other.gameObject.CompareTag("PlayerTag"))
        {
            other.gameObject.GetComponentInParent<NewPlayerScript>().HealthPickup(heal);
            Destroy(gameObject);
        }
    }
}
