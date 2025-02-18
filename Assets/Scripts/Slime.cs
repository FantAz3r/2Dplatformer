using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour, IDamageable
{
    [SerializeField] private float _patrolDuration = 2f;
    [SerializeField] private int _health = 10;
   

    private Rigidbody2D _rigidbody2D;
    private bool _isPatrolling;
    private Mover _mover;
    private Jumper _jumper;
    private GroundChecker _groundChecker;
    private PlayerFounder _playerFounder;
    private Attacker _attacker;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _groundChecker = GetComponent<GroundChecker>();
        _playerFounder = GetComponent<PlayerFounder>();
        _wait = new WaitForSeconds(_patrolDuration);
    }

    private void Start()
    {
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        if (_playerFounder.GetTarget() != null)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Character player))
        {
            _attacker.Attack(player);
        }
    }

    private void Jump()
    {
        _jumper.Jump(_groundChecker.IsGrounded(), _rigidbody2D);
    }

    private void Move(float direction)
    {
        _mover.Move(direction, _rigidbody2D);
    }


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

    private IEnumerator Patrol()
    {
        while (_playerFounder.GetTarget() == null) 
        {
            _isPatrolling = true;
            Move(SetRandomPatrolDirection());
            yield return _wait;
        }
    }

    private void ChasePlayer()
    {
        _isPatrolling = false;
        float direction = _playerFounder.GetTarget().position.x - transform.position.x;
        Jump();
        Move(direction);
    }

    private float SetRandomPatrolDirection()
    {
        float rightDirection = 1f;
        float leftDirection = -1f;

        return Random.Range(-leftDirection, rightDirection);
    }
}
