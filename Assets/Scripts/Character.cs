using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(AnimationUpdater))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(GroundDetecter))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Fliper))]


public class Character : MonoBehaviour
{
    private AnimationUpdater _animationUpdater;
    private Rigidbody2D _rigidbody;
    private InputService _inputService;
    private Mover _mover;
    private Jumper _jumper;
    private GroundDetecter _groundDetecter;
    private Health _health;
    private PlayerAttack _playerAttack;
    private Fliper _fliper;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputService = GetComponent<InputService>();
        _animationUpdater = GetComponent<AnimationUpdater>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _groundDetecter = GetComponent<GroundDetecter>();
        _health = GetComponent<Health>();
        _playerAttack = GetComponent<PlayerAttack>();
        _fliper = GetComponent<Fliper>();
    }

    private void OnEnable()
    {
        _inputService.Jumped += Jump;
        _inputService.MovedLeft += Move;
        _inputService.MovedRight += Move;
        _inputService.MouseButtonPushed += Attack;
        _inputService.MouseMoved += RotateCharacterToMouse;
        _groundDetecter.Grounded += SetGrounded;
    }

    private void OnDisable()
    {
        _inputService.Jumped -= Jump;
        _inputService.MovedLeft -= Move;
        _inputService.MovedRight -= Move;
        _inputService.MouseButtonPushed -= Attack;
        _inputService.MouseMoved -= RotateCharacterToMouse;
        _groundDetecter.Grounded -= SetGrounded;
    }

    private void Move(float direction)
    {
        _mover.Move(direction, _rigidbody);
        _animationUpdater.PlayMove(_rigidbody.velocity.x);
    }

    private void Jump()
    {
        if (_groundDetecter.IsGrounded)
        {
            _animationUpdater.PlayJump();
            _jumper.Jump(_rigidbody);
        }
    }

    private void Attack(Vector2 mousePosition)
    {
        _playerAttack.Attack(mousePosition);
    }

    private void RotateCharacterToMouse(float mousePositionX)
    {
        float direction = mousePositionX - transform.position.x; 
        _fliper.Flip(direction);
    }

    public void SetGrounded()
    {
        _animationUpdater.SetGrounded(true);
    }
}
