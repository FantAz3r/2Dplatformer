using System;
using System.Collections;
using UnityEngine;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private float _damage = 0.5f;
    [SerializeField] private float _coolDown = 4f;
    [SerializeField] private float _activeTime = 6f;
    [SerializeField] private float _damageRate = 0.5f;
    [SerializeField] private GameObject _radiusIndicatorPrefab;
    [SerializeField] private EntityDetecter _entityDetector;

    private Health _health;
    private bool _isActive = false;
    private GameObject _radiusIndicator;
    private WaitForSeconds _timer;
    private float currentCoolDown = 0;

    public event Action<float> AbilityEnabled;
    public event Action<float> CooldownStarted;


    private void Awake()
    {
        _timer = new WaitForSeconds(_damageRate);
        _health = GetComponent<Health>();
        _entityDetector = GetComponent<EntityDetecter>();
        _radiusIndicator = Instantiate(_radiusIndicatorPrefab, transform.position, Quaternion.identity);
        _radiusIndicator.transform.SetParent(transform);
        _radiusIndicator.SetActive(false);
    }

    public void ActivateAbility()
    {
        if (_isActive == false && currentCoolDown <= 0)
        {
            _isActive = true;
            _radiusIndicator.SetActive(true);
            AbilityEnabled?.Invoke(_activeTime);
            StartCoroutine(HandleVampirism());
        }
    }

    private IEnumerator HandleVampirism()
    {
        float currentActiveTime = _activeTime;

        while (currentActiveTime > 0)
        {
            HandleAbility();
            currentActiveTime -= _damageRate;
            yield return _timer;
        }

        DeactivateAbility();
    }

    private void HandleAbility()
    {
        Transform nearestEnemy = _entityDetector.GetNearestEnemy();

        if (nearestEnemy != null)
        {

            Health enemyHealth = nearestEnemy.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage);
                _health.Heal(_damage);
            }
        }
    }

    private void DeactivateAbility()
    {
        _isActive = false;
        _radiusIndicator.SetActive(false);
        CooldownStarted?.Invoke(_coolDown);
        StartCoroutine(HandleCooldown());
    }

    private IEnumerator HandleCooldown()
    {
        currentCoolDown = _coolDown;

        while (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime; 
            yield return null; 
        }
    }
}


