using UnityEngine;

public class AnimationUpdater : MonoBehaviour
{
    private const string Speed = nameof(Speed);
    private const string IsGrounded = nameof(IsGrounded);
    private const string Jump = nameof(Jump);

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

    public void PlayMove(Rigidbody2D rigidbody)
    {
        _animator.SetFloat(Speed, Mathf.Abs(rigidbody.velocity.x));
    }

    public void SetGroundedTrigger(bool isGrounded)
    {
        if (isGrounded)
        {
            _animator.SetBool("IsGrounded", true);
        }
        else
        {
            _animator.SetBool("IsGrounded", false);
        }
    }
        
}
