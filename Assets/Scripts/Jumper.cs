using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _gravityScale = 9.8f;

    public void Jump(Rigidbody2D rigidbody)
    {
            rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            rigidbody.gravityScale = _gravityScale;
    }
}
