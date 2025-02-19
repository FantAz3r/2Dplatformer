using UnityEngine;

public class Indicators : MonoBehaviour
{
    [SerializeField] private Transform _healthSprite;

    private Health _health;
    private float _indicatorWidth;

    private void Awake()
    {
        _indicatorWidth = _healthSprite.localScale.x;
        _health = GetComponent<Health>();
    }

    public void ViewIndicator(float currentHealth)
    {
        float healthScale = (currentHealth / _health.MaxHealth) * _indicatorWidth;
        float healthPosition = -((_health.MaxHealth - currentHealth) * _indicatorWidth / (2 * _health.MaxHealth));

        _healthSprite.localScale = new Vector2( healthScale, _healthSprite.localScale.y);
        _healthSprite.localPosition = new Vector2(healthPosition * transform.localScale.x, _healthSprite.localPosition.y);
    }
}
