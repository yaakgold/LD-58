using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth =  maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            // Death
            Destroy(gameObject);
        }
    }
}
