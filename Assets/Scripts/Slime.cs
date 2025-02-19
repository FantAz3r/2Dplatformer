using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
    }

    private void Start()
    {
        StartCoroutine(_patruller.Patrol());
    }

    private void Update()
    {
        if (_playerFounder.GetTarget() != null)
        {
            StopCoroutine(_patruller.Patrol());
            ChasePlayer();
        }
        else
        {
            if (_isPatrolling == false)
            {
                _isPatrolling = true;
                StartCoroutine(_patruller.Patrol());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Health target))
        {
            _attacker.Attack(target);
        }
    }

    private void Jump()
    {
        _jumper.Jump(_groundDetecter.IsGrounded(), _rigidbody2D);
    }

    private void Move(float direction)
    {
        _mover.Move(direction, _rigidbody2D);
    }

    private void ChasePlayer()
    {
        _isPatrolling = false;
        float direction = _playerFounder.GetTarget().position.x - transform.position.x;
        Jump();
        Move(direction);
    }
}
