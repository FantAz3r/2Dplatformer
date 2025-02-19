using UnityEngine;

public class GroundDetecter : MonoBehaviour
{
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }
}
