using TMPro;

public class TextBar : Indicator
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public override void ViewIndicator(float currentHealth)
    {
        _text.text = $"{((int)currentHealth)}/{Health.MaxHealth}";
    }
}
