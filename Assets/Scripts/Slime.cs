using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private float _patrolDuration = 2f;
    [SerializeField] private float _agrRange = 10f;
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private int _health = 10;
    [SerializeField] private int _damage = 1;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask groundLayer;

    private float groundCheckRadius = 0.1f;
    private bool _isGrounded;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Transform _target;
    private bool _isPatrolling;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        FindPlayer();

        if (_target != null)
        {
            StopCoroutine(Patrol());
            ChasePlayer();
        }
        else
        {
            if (_isPatrolling == false)
            {
                StartCoroutine(Patrol());
            }
        }
    }

    private void FindPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _agrRange);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<PlayerMovement>(out PlayerMovement player))
            {
                _target = player.transform;
                return; 
            }
        }

        _target = null;
    }

    private void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, groundCheckRadius, groundLayer);

        if (_isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Move(Vector2 direction)
    {
        _rigidbody2D.velocity = new Vector2(direction.x * _moveSpeed, _rigidbody2D.velocity.y);
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (_rigidbody2D.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_rigidbody2D.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            Attack(player);
        }
    }

    private void Attack(PlayerMovement player)
    {
        player.TakeDamage(_damage);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator Patrol()
    {
        while (_target == null) 
        {
            _isPatrolling = true;
            Move(SetRandomPatrolDirection());
            yield return new WaitForSeconds(_patrolDuration);
        }
    }

    private void ChasePlayer()
    {
        _isPatrolling = false;
        Vector2 direction = ((Vector2)_target.position - (Vector2)transform.position).normalized;
        Jump();
        Move(direction);
    }

    private Vector2 SetRandomPatrolDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), 0).normalized;
    }
}
