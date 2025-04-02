using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange = 5f; 
    [SerializeField] private LayerMask _enemyLayer;

    private Attacker _attacker;
    private Pusher _pusher;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _pusher = GetComponent<Pusher>();
    }

    public void Attack(Vector2 mousePosition)
    {
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _attackRange, _enemyLayer);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Health target))
            {
                _attacker.Attack(target);
                _pusher.PushBack(target);
            }
        }
    }
}
