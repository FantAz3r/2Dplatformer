using System.Collections;
using UnityEngine;

public class Patruller : MonoBehaviour
{
    [SerializeField] private float _patrolDuration = 2f;

    private Mover _mover;
    private Rigidbody2D _rigidbody2D;
    private WaitForSeconds _wait;
    private PlayerFounder _playerFounder;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _wait = new WaitForSeconds(_patrolDuration);
        _playerFounder = GetComponent<PlayerFounder>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public IEnumerator Patrol()
    {
        while (_playerFounder.GetTarget() == null)
        {
            _mover.Move(SetRandomPatrolDirection(), _rigidbody2D);
            yield return _wait;
        }
    }

    private float SetRandomPatrolDirection()
    {
        float rightDirection = 1f;
        float leftDirection = -1f;

        return Random.Range(-leftDirection, rightDirection);
    }
}
