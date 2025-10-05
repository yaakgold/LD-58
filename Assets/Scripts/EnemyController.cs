using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Transform[]  waypoints;

    protected int WaypointIndex = 0;
    protected bool IsAttacking = false;
    protected Collider2D _col;
    protected Rigidbody2D _rb;

    public Transform GetWaypoint(int index) => waypoints[index];
    
    private void Start()
    {
        foreach (var waypoint in waypoints)
        {
            waypoint.parent = null;
        }
        
        WaypointIndex = Random.Range(0, waypoints.Length);
        
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
