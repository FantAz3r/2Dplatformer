using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    [SerializeField] private float _moveAmplitude = 0.5f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 180f;

    private int _moneyPerCoin = 1;
    public int MoneyPerCoin => _moneyPerCoin;

    private void Update()
    {
        float newY = transform.localPosition.y + Mathf.Sin(Time.time * _moveSpeed) * _moveAmplitude;
        transform.localPosition = new Vector2(transform.localPosition.x, newY);
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }

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
