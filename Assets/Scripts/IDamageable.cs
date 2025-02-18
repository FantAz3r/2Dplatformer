public interface IDamageable
{
    public int health {  get; }
    public void TakeDamage(int damage);
    public void Die();
}

