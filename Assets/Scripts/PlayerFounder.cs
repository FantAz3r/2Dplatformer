using System;
using UnityEngine;

public class PlayerFounder : MonoBehaviour
{
    [SerializeField] private Transform _detecter;

    private Transform _target; 

    public event Action PlayerDetected;
    public event Action PlayerLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out _))
        {
            _target = collision.transform;
            PlayerDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out _))
        {
            _target = null;
            PlayerLost?.Invoke();   
        }
    }

    public Transform GetTarget()
    {
        return _target; 
    }
}

