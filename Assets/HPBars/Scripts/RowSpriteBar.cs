using UnityEngine;

public class RowSpriteBar : Indicator
{
    private RectTransform _healthSprite;
    private float _healthPercentage;
    private float _spriteSize;

    private void Awake()
    {
        _healthSprite = GetComponent<RectTransform>();
        _spriteSize = _healthSprite.sizeDelta.x;
    }

    public override void ViewIndicator(float currentHealth)
    {
        _healthPercentage = currentHealth / Health.MaxHealth;
        _healthSprite.sizeDelta = new Vector2(_healthPercentage * _spriteSize, _healthSprite.sizeDelta.y);
    }
}
