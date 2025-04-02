using System.Collections;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private Fliper _fliper;

    private Mover _mover;
    private Rigidbody2D _rigidbody2D;
    private Transform _target;
    private Coroutine _chaseCoroutine;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void StartChasing(Transform target)
    {
        _target = target;
        _chaseCoroutine ??= StartCoroutine(Chase());
    }

    public void StopChasing()
    {
        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }
    }

    private IEnumerator Chase()
    {
        while (enabled)
        {
            if (_target != null)
            {
                Vector2 direction = (_target.position - transform.position).normalized;
                _fliper.Flip(-direction.x);
                _mover.Move(direction.x, _rigidbody2D);
            }

            yield return null;
        }
    }
}

