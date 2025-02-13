using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _health = 100;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float _gravity = 5f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Jump();
        UpdateAnimation();
    }

    private void Move()
    {
        float move = Input.GetAxis(Horizontal);
        _rigidbody.velocity = new Vector2(move * _moveSpeed, _rigidbody.velocity.y);

        if (move > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (move < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
            _animator.SetBool("IsGrounded", false);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _rigidbody.gravityScale = _gravity;
        }
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));

        if (_isGrounded)
        {
            _animator.SetBool("IsGrounded", true);
        }
        else
        {
            _animator.SetBool("IsGrounded", false);
        }
    }
}
