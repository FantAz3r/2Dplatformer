using System.Collections;
using UnityEngine;

public abstract class SmoothIndicator : Indicator
{
    private RectTransform _healthSprite;

    private Coroutine _currentCoroutine;
    protected float _smoothHealth;
    protected float _healthPercentage;
    protected float _spriteSize;

    protected RectTransform HealthSprite => _healthSprite;

    private void Awake()
    {
        _healthSprite = GetComponent<RectTransform>();
        _smoothHealth = Health.MaxHealth;
        _spriteSize = _healthSprite.sizeDelta.x;
    }

    public override void ViewIndicator(float currentHealth)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(SmoothHealthTransition(currentHealth));
    }

    protected abstract IEnumerator SmoothHealthTransition(float targetHealth);
}
