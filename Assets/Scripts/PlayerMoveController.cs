using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _sprintSpeed = 7f;
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _groundDistance = 0.25f;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _characterController;
    private Vector2 _movementInput;
    private Vector3 _verticalVelocity;
    private bool _isGrounded;

    private void Reset()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
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
        bool isSprinting = _movementInput.magnitude > 0.85f;

        float speed = isSprinting ? _sprintSpeed : _walkSpeed;
        _characterController.Move(moveDirection * (speed * Time.deltaTime));
    }

    private void HandleGravity()
    {
        _verticalVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (!_isGrounded) return;

        _verticalVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    }
}
