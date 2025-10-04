using System;
using DG.Tweening;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Vector3 startAngle = new Vector3(0, 0, 70);
    [SerializeField] private Vector3 endAngle = new Vector3(0, 0, -70);
    [SerializeField] private float swingDuration = 0.2f;
    [SerializeField] private Ease swingEase = Ease.OutQuad;
    [SerializeField] private float knockbackForce;

    private bool _isSwinging = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isSwinging || !other.CompareTag("Enemy")) return;
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

    public void SwingWeapon()
    {
        if (_isSwinging) return;
        
        _isSwinging = true;
        // Set the starting rotation instantly
        transform.localEulerAngles = startAngle;

        // Tween the rotation to the end angle
        transform.DOLocalRotate(endAngle, swingDuration)
            .SetEase(swingEase)
            .OnComplete(() =>
            {
                // Reset to the start position or idle state
                transform.DOLocalRotate(startAngle, swingDuration)
                    .SetEase(Ease.OutBounce)
                    .OnComplete(() => _isSwinging = false);
            });
    }
}
