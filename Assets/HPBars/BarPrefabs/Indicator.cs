using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    [SerializeField] private  Health _health;

    protected Health Health => _health;

    private void OnEnable()
    {
        _health.IsValueChange += ViewIndicator;
    }

    private void OnDisable()
    {
        _health.IsValueChange -= ViewIndicator;
    }

    public abstract void ViewIndicator(float currentHealth);
}
