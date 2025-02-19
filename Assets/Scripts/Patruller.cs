using System.Collections;
using UnityEngine;

public class Patruller : MonoBehaviour
{
    [SerializeField] private float _patrolDuration = 2f;

    private Mover _mover;
    private Rigidbody2D _rigidbody2D;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _wait = new WaitForSeconds(_patrolDuration);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void StartPatrol()
    {
        StartCoroutine(Patrol());
    }

    public void StopPatrol()
    {
        StopCoroutine(Patrol());  
    }

    private IEnumerator Patrol()
    {
        while (enabled) 
        {
            _mover.Move(SetRandomPatrolDirection(), _rigidbody2D);
            yield return _wait;
        }
    }

    private float SetRandomPatrolDirection()
    {
        return Random.Range(-1f, 1f); 
    }
}
