using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Transform[]  waypoints;
    [SerializeField] protected Health health;
    [SerializeField] private GameObject selfPref;
    [SerializeField] protected GameObject gfx;

    protected int WaypointIndex = 0;
    protected bool IsAttacking = false;
    protected Collider2D _col;
    protected Rigidbody2D _rb;
    protected bool _isDead = false;

    private Vector3 _startPos;
    
    public Transform GetWaypoint(int index) => waypoints[index];
    
    private void Start()
    {
        _startPos = transform.position;
        
        health = GetComponent<Health>();
        health.OnDeath.AddListener(OnDeath);
        
        speed += Random.Range(-0.1f,0.2f);
        
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

    private void OnDeath()
    {
        _isDead = true;
        gfx.SetActive(false);
        StartCoroutine(WaitToRespawn(Random.Range(10,25)));
    }

    private IEnumerator WaitToRespawn(int time)
    {
        yield return new WaitForSeconds(time);
        
        var go = Instantiate(selfPref, _startPos, Quaternion.identity);
        if (go.TryGetComponent(out EnemyController ec))
        {
            ec.GetWaypoint(0).position = waypoints[0].position;
            ec.GetWaypoint(1).position = waypoints[1].position;
            
            Destroy(waypoints[0].gameObject);
            Destroy(waypoints[1].gameObject);
        }
        
        Destroy(gameObject);
    }
}
