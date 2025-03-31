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

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_detecter.position, _dedecterRange, _enemyLayer);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<Health>(out _))
                {
                    if (_targets.Contains(collider.transform) == false)
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
