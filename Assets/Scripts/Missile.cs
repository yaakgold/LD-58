using System;
using DG.Tweening;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target;
    public float speed;

    private bool _isAttacking;
    
    private void Start()
    {
        Fire();
    }

    private void Update()
    {
        if (!_isAttacking || !target) return;
        
        
        var dir = target.transform.position - transform.position;
        var zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zAngle - 90);
        
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent(out Health health))
        {
            health.TakeDamage(2);
            
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        transform.DOMoveY(transform.position.y + 3f, 0.75f)
            .OnComplete(() =>
            {
                _isAttacking = true;
            });
    }
}
