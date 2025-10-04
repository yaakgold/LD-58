using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform[]  waypoints;

    private int _waypointIndex = 0;

    private void Start()
    {
        foreach (var waypoint in waypoints)
        {
            waypoint.parent = null;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[_waypointIndex].position) < 0.1f)
            _waypointIndex = (_waypointIndex + 1) % waypoints.Length;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[_waypointIndex].position, speed * Time.deltaTime);
    }
}
