using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    public void Move(float direction, Rigidbody2D rigidbody )
    {
        rigidbody.velocity = new Vector2(direction * _moveSpeed, rigidbody.velocity.y);

        if (direction != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
        }
        
    }
}
