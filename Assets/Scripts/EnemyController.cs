using System;
using System.Collections;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private static readonly int Dead = Animator.StringToHash("Dead");
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
    
    public Animator anim;
    
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
        if (_isDead || UIManager.Instance.gameOver) return;
        if (Vector3.Distance(transform.position, waypoints[WaypointIndex].position) < 0.1f)
            WaypointIndex = (WaypointIndex + 1) % waypoints.Length;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[WaypointIndex].position, speed * Time.deltaTime);
    }

    private void OnDeath()
    {
        _isDead = true;
        anim.SetTrigger(Dead);
        Destroy(waypoints[0].gameObject);
        Destroy(waypoints[1].gameObject);
        
        Destroy(gameObject, 30);
    }
}
