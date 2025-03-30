using System;
using System.Collections.Generic;
using UnityEngine;

public  class EntityDetecter : MonoBehaviour
{
    [SerializeField] private Transform _detecter;

    private List<Transform> _targets;

    public event Action EntityDetected;
    public event Action EntityLost;

    private void Awake()
    {
        _targets = new List<Transform>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out _))
        {
            _targets.Add(collision.transform);
            EntityDetected?.Invoke();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out _))
        {
            _targets.Remove(collision.transform);
            EntityLost?.Invoke();
        }
    }

    public Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Transform target in _targets)
        {
            float distance = Vector2.Distance(target.position, transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = target;
            }
        }

        return nearestEnemy;
    }
}
