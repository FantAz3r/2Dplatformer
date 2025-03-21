using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(PlayerFounder))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(GroundDetecter))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Patruller))]

public class Slime : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _isPatrolling;
    private Mover _mover;
    private Jumper _jumper;
    private GroundDetecter _groundDetecter;
    private PlayerFounder _playerFounder;
    private Attacker _attacker;
    private Health _health;
    private Patruller _patruller;
    private Pusher _pusher;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _groundDetecter = GetComponent<GroundDetecter>();
        _playerFounder = GetComponent<PlayerFounder>();
        _health = GetComponent<Health>();
        _patruller = GetComponent<Patruller>();
        _pusher = GetComponent<Pusher>();
    }

    private void Update()
    {

        if (_playerFounder.GetTarget() != null)
        {
            _patruller.StopPatrol();
            ChasePlayer();
        }
        else
        {
            if (_isPatrolling == false)
            {
                _isPatrolling = true;
                _patruller.StartPatrol();
            }
        }
    }

    private void Jump()
    {
        if (_groundDetecter.IsGrounded() == true)
        {
            _jumper.Jump(_rigidbody2D);
        }
    }

    private void Move(float direction)
    {
        if (direction != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(direction), 1);
        }

        _mover.Move(direction, _rigidbody2D);
    }

    private void ChasePlayer()
    {
        _isPatrolling = false;
        float direction = _playerFounder.GetTarget().position.x - transform.position.x;
        Jump();
        Move(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Health target))
        {
            _attacker.Attack(target);
            _pusher.PushBack(target);
        }
    }
}
