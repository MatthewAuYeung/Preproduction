using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public TpsCameraScript tpsCam;
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

    private HandScript _hand;
    private bool aiming;
    private void Awake()
    {
        //_camera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        _hand = GetComponentInChildren<HandScript>();
        aiming = false;
    }

    private void Update()
    {
        CheckAiming();
        Aim();
        Movement();
        Fire();
    }

    private void FixedUpdate()
    {
        if(!aiming)
        {
            float heading = Mathf.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg;

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                lookdir = new Vector3(0.0f, heading, 0.0f);
                Quaternion lookRotation = Quaternion.Euler(0, heading, 0);
                Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                rb.rotation = Quaternion.Euler(0, rotation.y, 0);
            }
        }
        rb.velocity = movement;
    }

    private void Movement()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed) * speedmultiplier;
        }
        else if(aiming)
            movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed) * 0.5f;
        else
            movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);
    }

    private void CheckAiming()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (aiming)
            {
                aiming = false;
            }
            else
            {
                aiming = true;
            }
            tpsCam.ChangeActive(aiming);
        }
    }

    void Aim()
    {
        if (Input.GetKey(KeyCode.Escape))
            Screen.lockCursor = false;
        else
            Screen.lockCursor = true;

        //if (Input.GetMouseButton(0))
        //{
        //    //_player.Attack();
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (_hand != null)
        //    {
        //        //_camera.transform.SetParent(_hand.transform);
        //        //_camera.transform.localRotation = _hand.transform.Find("Aimmer").transform.localRotation;
        //        //_camera.transform.localPosition = _hand.transform.Find("Aimmer").transform.localPosition - new Vector3(0.0f, 3.0f, 0.0f);
        //        aiming = true;

        //    }
        //}

        //if (Input.GetMouseButtonUp(1))
        //{
        //    //_camera.transform.SetParent(gameObject.transform);
        //   // _camera.transform.localRotation = new Quaternion();
        //    //_camera.transform.localPosition = new Vector3(0.0f, 1.0f, -5.0f);
        //    aiming = false;
        //}

        if (aiming)
        {

            _hand.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * 100.0f, 0.0f, 0.0f));
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * Time.deltaTime * 300.0f, 0.0f));
            //Debug.Log(Input.GetAxis("Mouse Y"));
        }

    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _hand.Action();
        }
    }
}
