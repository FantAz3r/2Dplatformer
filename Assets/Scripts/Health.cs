using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10f;
    [SerializeField] private float _currentHealth;

    public float MaxHealth => _maxHealth;

    public event Action<float> IsDamageTaken;
    public event Action Died;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        IsDamageTaken?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Died?.Invoke();
        Destroy(gameObject);
    }
}
