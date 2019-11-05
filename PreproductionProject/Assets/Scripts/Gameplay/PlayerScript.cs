using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private float speedmultiplier = 1.5f;
    //private Camera _camera;
    private Vector3 movement;
    private Rigidbody rb;
    private float timer;
    private int weaponmode;

    private void Awake()
    {
        //_camera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        timer = 50.0f;
        weaponmode = 1;
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed) * speedmultiplier;
        }
        else
            movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);

        //Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayLength;

        //if (groundPlane.Raycast(cameraRay, out rayLength))
        //{
        //    Vector3 pointToLook = cameraRay.GetPoint(rayLength);

        //    pointToLook.y = transform.position.y;
        //    transform.LookAt(pointToLook);
        //}
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }

    public float GetTimer()
    {
        return timer;
    }

    public int GetWeaponMode()
    {
        return weaponmode;
    }
}
