using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private float _healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            if (collision.TryGetComponent<Character>(out _))
            {
                health.Heal(_healAmount);
                Destroy(gameObject);
            }
        }
    }
}
