using System;
using DG.Tweening;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;
    [SerializeField] private PlayerController player;

    public bool isSwinging;

    private GameObject _enemy;
    
    private void Update()
    {
        if (!isSwinging || !_enemy) return;
        
        if (_enemy.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        if (_enemy && _enemy.CompareTag("Enemy") && _enemy.TryGetComponent(out Rigidbody2D rb))
        {
            var dir = _enemy.transform.position - transform.position;
            dir.y = 1;
            rb.AddForce(dir.normalized * knockbackForce, ForceMode2D.Impulse);
        }

        isSwinging = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isSwinging) return;
        
        if (!other.CompareTag("Enemy") && !other.CompareTag("Dragon")) return;
        
        _enemy = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != _enemy) return;

        _enemy = null;
    }
}
