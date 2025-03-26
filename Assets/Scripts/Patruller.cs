using System.Collections;
using UnityEngine;

public class Patruller : MonoBehaviour
{
    [SerializeField] private float _patrolDuration = 2f;

    private Mover _mover;
    private Fliper _fliper;
    private Rigidbody2D _rigidbody2D;
    private WaitForSeconds _wait;
    private Jumper _jumper;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _wait = new WaitForSeconds(_patrolDuration);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _fliper = GetComponent<Fliper>();
        _jumper = GetComponent<Jumper>();
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
            _fliper.Flip(SetRandomPatrolDirection());
            _jumper.Jump(_rigidbody2D);
            yield return _wait;
        }
    }

    private float SetRandomPatrolDirection()
    {
        return Random.Range(-1, 1); 
    }
}
