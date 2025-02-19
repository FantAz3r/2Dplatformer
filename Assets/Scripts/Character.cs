using UnityEngine;

public class Character : MonoBehaviour
{
    private AnimationUpdater _animationUpdater;
    private Rigidbody2D _rigidbody;
    private InputService _inputService;
    private Mover _mover;
    private Jumper _jumper;
    private GroundDetecter _groundDetecter;
    private Health _health;
    private Indicators _indicators;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputService = GetComponent<InputService>();
        _animationUpdater = GetComponent<AnimationUpdater>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _groundDetecter = GetComponent<GroundDetecter>();
        _health = GetComponent<Health>();
        _indicators = GetComponent<Indicators>();
    }

    private void OnEnable()
    {
        _inputService.Jumped += Jump;
        _inputService.MovedLeft += Move;
        _inputService.MovedRight += Move;
        _health.IsDamageTaken += UpdateHealthIndicator;
    }

    private void Update()
    {
        _animationUpdater.SetGroundedTrigger(_groundDetecter.IsGrounded());
    }

    private void OnDisable()
    {
        _inputService.Jumped -= Jump;
        _inputService.MovedLeft -= Move;
        _inputService.MovedRight -= Move;
        _health.IsDamageTaken -= UpdateHealthIndicator;
    }

    private void Move(float direction)
    {
        _mover.Move(direction, _rigidbody);
        _animationUpdater.PlayMove(_rigidbody);
    }

    private void Jump()
    {
        _animationUpdater.PlayJump();
        _jumper.Jump(_groundDetecter.IsGrounded(), _rigidbody);
    }

    private void UpdateHealthIndicator(float currentHealth)
    {
        _indicators.ViewIndicator(currentHealth);
    }
}
