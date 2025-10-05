using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    public UnityEvent OnDeath;
    
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth =  maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        print(name + " " + _currentHealth);
        if (_currentHealth <= 0)
        {
            OnDeath.Invoke();
        }
    }

    public void SetHealth(int newHealth)
    {
        maxHealth = _currentHealth = newHealth;
    }
}
