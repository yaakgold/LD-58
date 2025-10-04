using DG.Tweening;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;

    public bool isSwinging = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isSwinging || !other.CompareTag("Enemy")) return;
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        if (other.TryGetComponent(out Rigidbody2D rb))
        {
            var dir = other.transform.position - transform.position;
            dir.y = 1;
            rb.AddForce(dir.normalized * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
