using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
