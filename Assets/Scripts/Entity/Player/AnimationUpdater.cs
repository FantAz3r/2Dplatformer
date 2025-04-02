using UnityEngine;

public class AnimationUpdater : MonoBehaviour
{
    public static readonly int s_Speed = Animator.StringToHash(nameof(s_Speed));
    public static readonly int s_Jump = Animator.StringToHash(nameof(s_Jump));
    public static readonly int s_IsGrounded = Animator.StringToHash(nameof(s_IsGrounded));

    [SerializeField] private Animator _animator;

    public void PlayJump()
    {
        _animator.SetTrigger(s_Jump);
        _animator.SetBool(s_IsGrounded, false);
    }

    public void PlayMove(float velocity)
    {
        _animator.SetFloat(s_Speed, Mathf.Abs(velocity));
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool(s_IsGrounded, isGrounded);
    }
}
