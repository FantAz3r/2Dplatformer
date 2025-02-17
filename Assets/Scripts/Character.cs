using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int _health = 10;

    private AnimationUpdater _animationUpdater;
    private Rigidbody2D _rigidbody;
    private InputService _inputService;
    private Mover _mover;
    private Jumper _jumper;
    private GroundChecker _groundChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputService = GetComponent<InputService>();
        _animationUpdater = GetComponent<AnimationUpdater>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _groundChecker = GetComponent<GroundChecker>();
    }

    private void OnEnable()
    {
        _inputService.Jump += Jump;
        _inputService.MoveLeft += Move;
        _inputService.MoveRight += Move;
    }

    private void Update()
    {
        _animationUpdater.SetGroundedTrigger(_groundChecker.IsGrounded());
    }

    private void OnDisable()
    {
        _inputService.Jump -= Jump;
        _inputService.MoveLeft -= Move;
        _inputService.MoveRight -= Move;
    }

    private void Move(float direction)
    {
        _mover.Move(direction, _rigidbody);
        _animationUpdater.MoveUpdate(_rigidbody);
    }

    private void Jump()
    {
        _animationUpdater.JumpAnimation();
        _jumper.Jump(_groundChecker.IsGrounded(), _rigidbody);
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
}
