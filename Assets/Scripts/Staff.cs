using DG.Tweening;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;

    public bool isSwinging;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isSwinging) return;

        isSwinging = false;
        
        if (!other.CompareTag("Enemy") && !other.CompareTag("Dragon")) return;
        
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        if (other.CompareTag("Enemy") && other.TryGetComponent(out Rigidbody2D rb))
        {
            var dir = other.transform.position - transform.position;
            dir.y = 1;
            rb.AddForce(dir.normalized * knockbackForce, ForceMode2D.Impulse);
        }
        else if (other.TryGetComponent(out BossController bc))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = true;
            bc.StopCoroutine(bc.BetweenTimerVar);
            bc.ChooseNextAttack();
        }
    }
}
