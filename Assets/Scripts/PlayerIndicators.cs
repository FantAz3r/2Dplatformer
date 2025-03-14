using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicators : MonoBehaviour
{
    [SerializeField] private RectTransform _healthSprite; 
    [SerializeField] private Slider _slider; 
    [SerializeField] private TMP_Text _text; 

    private Health _health; 
    private float _targetHealth; 
    private float _smoothHealth;
    private float _spriteSize;
    private float healthPercentage;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _targetHealth = _health.CurrentHealth;
        _spriteSize = _healthSprite.sizeDelta.x;
    }

    private void Update()
    {
        _smoothHealth = Mathf.MoveTowards(_smoothHealth, _targetHealth, Time.deltaTime * 5); 
        UpdateIndicators(_smoothHealth);
        UpdateIndicatorsRaw(_targetHealth);
    }

    public void ViewIndicator(float currentHealth)
    {
        _targetHealth = currentHealth; 
        UpdateIndicators(currentHealth); 
    }

    private void UpdateIndicators(float currentHealth)
    {
        healthPercentage = currentHealth / _health.MaxHealth;
        _text.text = $"{((int)currentHealth)}/{_health.MaxHealth}";
        _slider.value = healthPercentage;
    }

    private void UpdateIndicatorsRaw(float currentHealth)
    {
        healthPercentage = currentHealth / _health.MaxHealth;
        _healthSprite.sizeDelta = new Vector2(healthPercentage * _spriteSize, _healthSprite.sizeDelta.y);
    }
}




