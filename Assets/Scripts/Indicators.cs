using UnityEngine;

public class Indicators : MonoBehaviour
{
    [SerializeField] private RectTransform _healthSprite; 
    private Health _health;
    private float _indicatorWidth;

    private void Awake()
    {
        _indicatorWidth = _healthSprite.sizeDelta.x;
        _health = GetComponent<Health>();
    }

    public void ViewIndicator(float currentHealth)
    {
        float healthScale = (currentHealth / _health.MaxHealth) * _indicatorWidth;
        float healthPosition = -((_health.MaxHealth - currentHealth) * _indicatorWidth / (2 * _health.MaxHealth));

        _healthSprite.sizeDelta = new Vector2(healthScale, _healthSprite.sizeDelta.y);
        _healthSprite.anchoredPosition = new Vector2(healthPosition, _healthSprite.anchoredPosition.y);
    }
}


    //public void ViewIndicator(float currentHealth)
    //{
    //    float healthScale = (currentHealth / _health.MaxHealth) * _indicatorWidth;
    //    float healthPosition = -((_health.MaxHealth - currentHealth) * _indicatorWidth / (2 * _health.MaxHealth));
    //
    //    _healthSprite.localScale = new Vector2( healthScale, _healthSprite.localScale.y);
    //    _healthSprite.localPosition = new Vector2(healthPosition * transform.localScale.x, _healthSprite.localPosition.y);
    //}

