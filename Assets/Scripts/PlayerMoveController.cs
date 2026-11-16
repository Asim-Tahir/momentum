using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _runSpeed = 7f;
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _groundDistance = 0.25f;
    [SerializeField] private LayerMask _groundMask;

    private readonly string _isIdleParam = "IsIdle";
    private readonly string _isWalkingParam = "IsWalking";
    private readonly string _isFastRunningParam = "IsFastRunning";
    private readonly string _isJumpingParam = "IsJumping";
    private int _isIdleHash;
    private int _isWalkingHash;
    private int _isFastRunningHash;
    private int _isJumpingHash;

    private CharacterController _characterController;
    private Vector2 _movementInput;
    private Vector3 _verticalVelocity;
    private bool _isGrounded;

    private void Reset()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        _isIdleHash = Animator.StringToHash(_isIdleParam);
        _isWalkingHash = Animator.StringToHash(_isWalkingParam);
        _isFastRunningHash = Animator.StringToHash(_isFastRunningParam);
        _isJumpingHash = Animator.StringToHash(_isJumpingParam);

        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        HandleGroundCheck();
        ReadJoystickInput();
        HandleHorizontalMovement();
        HandleGravity();
    }

    private void HandleGroundCheck()
    {
        Vector3 checkPosition = _groundCheck != null ? _groundCheck.position : transform.position;
        _isGrounded = Physics.CheckSphere(checkPosition, _groundDistance, _groundMask);
        if (_isGrounded && _verticalVelocity.y < 0)
        {
            _verticalVelocity.y = -2f;
            _animator.SetBool(_isJumpingHash, false);
            _animator.SetBool(_isIdleHash, true);
        }
    }

    private void ReadJoystickInput()
    {
        if (_joystick == null) return;

        _movementInput = new(_joystick.Horizontal, _joystick.Vertical);
    }

    private void HandleHorizontalMovement()
    {
        Vector3 moveDirection = transform.right * _movementInput.x + transform.forward * _movementInput.y;
        bool isRunning = _movementInput.magnitude > 0.85f;
        bool isWalking = _movementInput.magnitude > 0.1f && !isRunning;

        float speed = isRunning ? _runSpeed : _walkSpeed;
        _characterController.Move(moveDirection * (speed * Time.deltaTime));

        if (_isGrounded && _verticalVelocity.y <= 0f)
        {
            _animator.SetBool(_isIdleHash, !isWalking && !isRunning);
            _animator.SetBool(_isWalkingHash, isWalking);
            _animator.SetBool(_isFastRunningHash, isRunning);
        }
    }

    private void HandleGravity()
    {
        _verticalVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (!_isGrounded || _verticalVelocity.y > 0f) return;

        _verticalVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        _animator.SetBool(_isIdleHash, false);
        _animator.SetBool(_isWalkingHash, false);
        _animator.SetBool(_isFastRunningHash, false);
        _animator.SetBool(_isJumpingHash, true);
    }
}
