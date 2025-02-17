using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void Attack(Character player)
    {
        player.TakeDamage(_damage);
    }
}
