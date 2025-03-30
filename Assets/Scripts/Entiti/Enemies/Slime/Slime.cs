using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(EntityDetecter))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(GroundDetecter))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Patruller))]

public class Slime : MonoBehaviour
{
    private EntityDetecter _entityDetecter;
    private Attacker _attacker;
    private Patruller _patruller;
    private Pusher _pusher;
    private Chaser _chaser;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _entityDetecter = GetComponent<EntityDetecter>();
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
        _entityDetecter.EntityDetected += OnPlayerDetected;
        _entityDetecter.EntityLost += OnPlayerLost;          
    }

    private void OnDisable()
    {
        _entityDetecter.EntityDetected -= OnPlayerDetected;
        _entityDetecter.EntityLost -= OnPlayerLost;          
    }

    private void OnPlayerDetected()
    {
        _patruller.StopPatrol();
        _chaser.StartChasing(_entityDetecter.GetNearestEnemy());
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
