using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationUpdater : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayJump()
    {
        _animator.SetTrigger(Jump);
        _animator.SetBool(IsGrounded, false);
    }

    public void PlayMove(float velocity)
    {
        _animator.SetFloat(Speed, Mathf.Abs(velocity));
    }

    public void SetGroundedTrigger(bool isGrounded)
    {
        _animator.SetBool(IsGrounded, isGrounded);
    }
}
