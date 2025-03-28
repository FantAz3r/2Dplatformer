using System.Collections;
using UnityEngine;

public class SmoothLerpSpriteBar : SmoothIndicator
{
    [SerializeField] private float _spriteSpeed = 0.05f;

    protected override IEnumerator SmoothHealthTransition(float targetHealth)
    {
        while (_smoothHealth != targetHealth)
        {
            _smoothHealth = Mathf.Lerp(_smoothHealth, targetHealth, _spriteSpeed);
            _healthPercentage = _smoothHealth / Health.MaxHealth;
            HealthSprite.sizeDelta = new Vector2(_healthPercentage * _spriteSize, HealthSprite.sizeDelta.y);
            yield return null;
        }
    }
}

