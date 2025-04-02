using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetecter : MonoBehaviour
{
    [SerializeField] private Transform _detecter;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _dedecterRange = 5f;
    [SerializeField] private float _checkInterval = 0.2f;
    [SerializeField] private int _maxDetectedEntitys = 3;

    private List<Transform> _targets;
    private WaitForSeconds _delay;

    public event Action EntityDetected;
    public event Action EntityLost;

    private void Awake()
    {
        _targets = new List<Transform>();
        _delay = new WaitForSeconds(_checkInterval);
    }

    private void Start()
    {
        StartCoroutine(DetectEnemies());
    }

    private IEnumerator DetectEnemies()
    {
        while (enabled)
        {
            _targets.Clear();

            Collider2D[] colliders = new Collider2D[_maxDetectedEntitys]; 
            int colliderCount = Physics2D.OverlapCircleNonAlloc(_detecter.position, _dedecterRange, colliders, _enemyLayer);

            for (int i = 0; i < colliderCount; i++)
            {
                Collider2D collider = colliders[i];
                if (collider.TryGetComponent<Health>(out _))
                {
                    if (!_targets.Contains(collider.transform))
                    {
                        _targets.Add(collider.transform);
                        EntityDetected?.Invoke(); 
                    }
                }
            }

            yield return _delay; 
        }
    }


    public Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float minDistanceSquared = float.MaxValue;

        foreach (Transform target in _targets)
        {
            float distanceSquared = (target.position - transform.position).sqrMagnitude;

            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                nearestEnemy = target;
            }
        }

        return nearestEnemy;
    }
}
