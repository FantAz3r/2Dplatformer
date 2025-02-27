using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Health target))
        {
            target.TakeDamage(_damage);
        }
    }
}
