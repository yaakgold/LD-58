using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Transform[]  waypoints;

    protected int WaypointIndex = 0;
    protected bool IsAttacking = false;
    protected Collider2D _col;
    protected Rigidbody2D _rb;

    private void Start()
    {
        foreach (var waypoint in waypoints)
        {
            waypoint.parent = null;
        }
        
        _col = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[WaypointIndex].position) < 0.1f)
            WaypointIndex = (WaypointIndex + 1) % waypoints.Length;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[WaypointIndex].position, speed * Time.deltaTime);
    }
}
