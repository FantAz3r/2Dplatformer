using UnityEngine;

public class AnimationUpdater : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void JumpAnimation()
    {
        _animator.SetTrigger("Jump");
        _animator.SetBool("IsGrounded", false);
    }

    public void MoveUpdate(Rigidbody2D rigidbody)
    {
        _animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));
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
