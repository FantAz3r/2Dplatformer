using UnityEngine;

public class MedKit : MonoBehaviour, ICollectible
{
    [SerializeField] private float _healAmount;
    public float HealAmount => _healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICollector>(out ICollector collector))
        {
            collector.Collect(this);
            Destroy(gameObject);
        }
    }

    public void Accept(ICollector collector)
    {
        collector.Collect(this);
    }
}
