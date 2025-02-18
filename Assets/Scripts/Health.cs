using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int health => _health;
    
    [SerializeField] private int _health = 10;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
