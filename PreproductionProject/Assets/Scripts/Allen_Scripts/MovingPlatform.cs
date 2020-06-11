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
    void Start()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        if (Waypoints.Length > 0)
        {
            current_target = Waypoints[0].position;
        }
        tolerance = speed * Time.deltaTime;
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
}
