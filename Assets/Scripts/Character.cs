using UnityEngine;

public class Character : MonoBehaviour
{
    private AnimationUpdater _animationUpdater;
    private Rigidbody2D _rigidbody;
    private InputService _inputService;
    private Mover _mover;
    private Jumper _jumper;
    private GroundChecker _groundChecker;
    private Health _health;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputService = GetComponent<InputService>();
        _animationUpdater = GetComponent<AnimationUpdater>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _groundChecker = GetComponent<GroundChecker>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _inputService.Jumped += Jump;
        _inputService.MovedLeft += Move;
        _inputService.MovedRight += Move;
    }

    private void Update()
    {
        _animationUpdater.SetGroundedTrigger(_groundChecker.IsGrounded());
    }

    private void OnDisable()
    {
        _inputService.Jumped -= Jump;
        _inputService.MovedLeft -= Move;
        _inputService.MovedRight -= Move;
    }

    private void Move(float direction)
    {
        _mover.Move(direction, _rigidbody);
        _animationUpdater.PlayMove(_rigidbody);
    }

    private void Jump()
    {
        _animationUpdater.PlayJump();
        _jumper.Jump(_groundChecker.IsGrounded(), _rigidbody);
    }

}
