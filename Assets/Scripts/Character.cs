using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(AnimationUpdater))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(GroundDetecter))]
[RequireComponent(typeof(Health))]

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
    }

    private void OnEnable()
    {
        _inputService.Jumped += Jump;
        _inputService.MovedLeft += Move;
        _inputService.MovedRight += Move;
        _inputService.MouseButtonPushed += Attack;
    }

    private void Update()
    {
        _animationUpdater.SetGroundedTrigger(_groundDetecter.IsGrounded());
        RotateCharacterToMouse();
    }

    private void OnDisable()
    {
        _inputService.Jumped -= Jump;
        _inputService.MovedLeft -= Move;
        _inputService.MovedRight -= Move;
        _inputService.MouseButtonPushed += Attack;
    }

    private void Move(float direction)
    {
        _mover.Move(direction, _rigidbody);
        _animationUpdater.PlayMove(_rigidbody.velocity.x);
    }

    private void Jump()
    {
        if (_groundDetecter.IsGrounded() == true)
        {
            _animationUpdater.PlayJump();
            _jumper.Jump(_rigidbody);
        }
    }

    private void Attack(Vector2 mousePosition)
    {
        _playerAttack.Attack(mousePosition);
    }

    private void RotateCharacterToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
    }
}
