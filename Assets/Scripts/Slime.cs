using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(PlayerFounder))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(GroundDetecter))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Patruller))]
[RequireComponent(typeof(Fliper))]

public class Slime : MonoBehaviour
{
    private PlayerFounder _playerFounder;
    private Attacker _attacker;
    private Patruller _patruller;
    private Pusher _pusher;
    private Chaser _chaser;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _playerFounder = GetComponent<PlayerFounder>();
        _patruller = GetComponent<Patruller>();
        _pusher = GetComponent<Pusher>();
        _chaser = GetComponent<Chaser>();
    }

    private void Start()
    {
        _patruller.StartPatrol();
    }

    private void OnEnable()
    {
        _playerFounder.PlayerDetected += OnPlayerDetected;  
        _playerFounder.PlayerLost += OnPlayerLost;          
    }

    private void OnDisable()
    {
        _playerFounder.PlayerDetected -= OnPlayerDetected;  
        _playerFounder.PlayerLost -= OnPlayerLost;          
    }

    private void OnPlayerDetected()
    {
        _patruller.StopPatrol();
        _chaser.StartChasing(_playerFounder.GetTarget());
    }

    private void OnPlayerLost()
    {
        _patruller.StartPatrol();
        _chaser.StopChasing();
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
