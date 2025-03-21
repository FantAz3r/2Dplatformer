using System.Collections;
using UnityEngine;

public class SmoothTowardsSpriteBar : SmoothIndicator
{
    [SerializeField] private float _spriteSpeed = 10f;

    protected override IEnumerator SmoothHealthTransition(float targetHealth)
    {
        while (_smoothHealth != targetHealth)
        {
            _smoothHealth = Mathf.MoveTowards(_smoothHealth, targetHealth, Time.deltaTime * _spriteSpeed);
            _healthPercentage = _smoothHealth / Health.MaxHealth;
            HealthSprite.sizeDelta = new Vector2(_healthPercentage * _spriteSize, HealthSprite.sizeDelta.y);
            yield return null;
        }
    }
}
