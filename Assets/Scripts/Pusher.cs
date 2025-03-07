using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] private float _pushForce = 5f; 

    public void PushBack(Health target)
    {
        if (target.TryGetComponent(out Rigidbody2D rigidbody))
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            rigidbody.AddForce(direction * _pushForce, ForceMode2D.Impulse);
        }
    }
}
