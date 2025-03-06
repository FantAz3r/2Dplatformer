using UnityEngine;

public class PlayerFounder : MonoBehaviour
{
    [SerializeField] private Transform _detecter;

    private Transform _target; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out _))
        {
            _target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out _))
        {
            _target = null;
        }
    }

    public Transform GetTarget()
    {
        return _target; 
    }
}

