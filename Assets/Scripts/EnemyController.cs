using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] protected Transform[]  waypoints;

    protected int WaypointIndex = 0;
    protected bool IsAttacking = false;

    private void Start()
    {
        foreach (var waypoint in waypoints)
        {
            waypoint.parent = null;
        }
    }

    public virtual void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[WaypointIndex].position) < 0.1f)
            WaypointIndex = (WaypointIndex + 1) % waypoints.Length;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[WaypointIndex].position, speed * Time.deltaTime);
    }
}
