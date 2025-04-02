using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(AnimationUpdater))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]

public class Character : MonoBehaviour
{
    [SerializeField] private Fliper _fliper;
    [SerializeField] private GroundDetecter _groundDetecter;

    private AnimationUpdater _animationUpdater;
    private Rigidbody2D _rigidbody;
    private InputService _inputService;
    private Mover _mover;
    private Jumper _jumper;
    private PlayerAttack _playerAttack;
    private VampireAbility _vampireAbility;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputService = GetComponent<InputService>();
        _animationUpdater = GetComponent<AnimationUpdater>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _playerAttack = GetComponent<PlayerAttack>();
        _vampireAbility = GetComponent<VampireAbility>();
    }

    private void OnEnable()
    {
        _inputService.Jumped += Jump;
        _inputService.MovedLeft += Move;
        _inputService.MovedRight += Move;
        _inputService.MouseButtonPushed += Attack;
        _inputService.MouseMoved += RotateCharacterToMouse;
        _groundDetecter.Grounded += SetGrounded;
        _inputService.AbilityActivated += UseAbility;
    }

    private void OnDisable()
    {
        _inputService.Jumped -= Jump;
        _inputService.MovedLeft -= Move;
        _inputService.MovedRight -= Move;
        _inputService.MouseButtonPushed -= Attack;
        _inputService.MouseMoved -= RotateCharacterToMouse;
        _groundDetecter.Grounded -= SetGrounded;
        _inputService.AbilityActivated -= UseAbility;
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

    private void UseAbility()
    {
        _vampireAbility.ActivateAbility();
    }
}
