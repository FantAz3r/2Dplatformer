public interface IDamageable
{
    public float MaxHealth {  get; }
    public void TakeDamage(int damage);
    public void Die();
}

