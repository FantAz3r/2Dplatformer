using UnityEngine;

public class AnimationUpdater : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));

    [SerializeField] private Animator _animator;

    public void PlayJump()
    {
        _animator.SetTrigger(Jump);
        _animator.SetBool(IsGrounded, false);
    }

    public void PlayMove(float velocity)
    {
        _animator.SetFloat(Speed, Mathf.Abs(velocity));
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool(IsGrounded, isGrounded);
    }
}
