using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float turnSpeed = 10.0f;
    [SerializeField]
    private float speed = 5.0f;

    private float speedmultiplier = 1.5f;
    //private Camera _camera;
    private Vector3 movement;
    private Rigidbody rb;
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 lookdir;
    private void Awake()
    {
        //_camera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed) * speedmultiplier;
            //movement = new Vector3(0.0f , 0.0f, moveVertical * speed) * speedmultiplier;
        }
        else
            movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);
        //movement = new Vector3(0.0f, 0.0f, moveVertical * speed

        #region Crap
        //Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayLength;

        //if (groundPlane.Raycast(cameraRay, out rayLength))
        //{
        //    Vector3 pointToLook = cameraRay.GetPoint(rayLength);

        //    pointToLook.y = transform.position.y;
        //    transform.LookAt(pointToLook);
        //}
        #endregion
    }

    private void FixedUpdate()
    {
        float heading = Mathf.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg;

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            lookdir = new Vector3(0.0f, heading, 0.0f);
            Quaternion lookRotation = Quaternion.Euler(0, heading, 0);
            Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            rb.rotation = Quaternion.Euler(0, rotation.y, 0);
        }
        rb.velocity = movement;
    }
}
