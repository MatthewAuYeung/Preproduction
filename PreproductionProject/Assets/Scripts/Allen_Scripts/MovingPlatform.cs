using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int point_number = 0;
    private Vector3 current_target;

    public Transform[] Waypoints;

    public float tolerance;
    public float speed;
    public float delay_time;

    private float delay_start;

    public bool automatic;
    private float originalSpeed;
    private bool isSlowed;
    private float timer;

    [SerializeField]
    private float slowTimer = 5.0f;

    void Start()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        if (Waypoints.Length > 0)
        {
            current_target = Waypoints[0].position;
        }
        tolerance = speed * Time.deltaTime;
        originalSpeed = speed;
    }

    void FixedUpdate()
    {
        if (transform.position != current_target)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    private void Update()
    {
        if(isSlowed)
        {
            if(timer < Time.time)
            {
                isSlowed = false;
                speed = originalSpeed;
            }
        }
    }

    void MovePlatform()
    {
        Vector3 heading = current_target - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = current_target;
            delay_start = Time.time;
        }
    }

    void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - delay_start > delay_time)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        point_number++;
        if (point_number >= Waypoints.Length)
        {
            point_number = 0;
        }
        current_target = Waypoints[point_number].position;
    }

    public void SlowFromBomb()
    {
        if(!isSlowed)
        {
            speed *= 0.5f;
            isSlowed = true;
            timer = Time.time + slowTimer;
        }
    }
}
