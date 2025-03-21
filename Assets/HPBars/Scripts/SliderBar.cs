using UnityEngine.UI;

public class SliderBar : Indicator
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public override void ViewIndicator(float currentHealth)
    {
        _slider.value = currentHealth / Health.MaxHealth;
    }
}
