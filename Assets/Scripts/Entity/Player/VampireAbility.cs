using System;
using System.Collections;
using UnityEngine;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private float _damage = 0.5f;
    [SerializeField] private float _coolDown = 4f;
    [SerializeField] private float _activeTime = 6f;
    [SerializeField] private SpriteRenderer _radiusIndicatorPrefab;
    [SerializeField] private EntityDetecter _entityDetector;

    private Health _health;
    private bool _isActive = false;
    private SpriteRenderer _radiusIndicator;
    private float currentCoolDown = 0;

    public event Action<float,float> TimePassed;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _entityDetector = GetComponent<EntityDetecter>();
        _radiusIndicator = Instantiate(_radiusIndicatorPrefab, transform.position, Quaternion.identity);
        _radiusIndicator.transform.SetParent(transform);
        _radiusIndicator.gameObject.SetActive(false);
    }

    public void ActivateAbility()
    {
        if (_isActive == false && currentCoolDown <= 0)
        {
            _isActive = true;
            _radiusIndicator.gameObject.SetActive(true);
            StartCoroutine(HandleVampirism());
        }
    }

    private IEnumerator HandleVampirism()
    {
        float currentActiveTime = 0;

        while (currentActiveTime < _activeTime)
        {
            float oneSecond = 0f;

            while (oneSecond < 1)
            {
                oneSecond += Time.deltaTime;
                TimePassed?.Invoke(_activeTime, currentActiveTime + oneSecond);
                yield return null;
            }

            HandleAbility();
            currentActiveTime++;
        }

        DeactivateAbility();
    }

    private void HandleAbility()
    {
        Transform nearestEnemy = _entityDetector.GetNearestEnemy();

        if (nearestEnemy != null)
        {
            nearestEnemy.TryGetComponent(out Health enemyHealth);
            float enemyCurrentHealth = enemyHealth.CurrentHealth;
            float damageDealt = Mathf.Min(_damage, enemyCurrentHealth);
            enemyHealth.TakeDamage(damageDealt);
            _health.Heal(damageDealt);
        }
    }

    private void DeactivateAbility()
    {
        _isActive = false;
        _radiusIndicator.gameObject.SetActive(false);
        StartCoroutine(HandleCooldown());
    }


    private IEnumerator HandleCooldown()
    {
        currentCoolDown = _coolDown;

        while (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
            TimePassed?.Invoke(_coolDown, currentCoolDown);
            yield return null;
        }
    }
}


