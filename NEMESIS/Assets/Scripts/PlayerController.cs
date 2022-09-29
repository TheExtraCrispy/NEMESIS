using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerControls _playerControls;
    private Rigidbody2D rb;

    [SerializeField] float _acceleration;
    [SerializeField] float _maxMoveSpeed;
    [SerializeField] float _dashForce;
    [SerializeField] float _stationaryDrag;
    [SerializeField] float _movingDrag;

    private Vector2 _currentVelocity;
    private Vector2 _moveDirection;
    private Vector2 _rotateDirection;
    private Vector2 _force;

    private InputAction move;
    private InputAction rotate;
    private InputAction dash;



    private void MoveForce()
    {
        _force = ((rb.velocity.magnitude <= _maxMoveSpeed) ? _moveDirection * _acceleration : _moveDirection * 0);
    }

    private void RotationTorque()
    {

    }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move = _playerControls.Player.Move;
        move.Enable();

        rotate = _playerControls.Player.Rotate;
        rotate.Enable();

        dash = _playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }

    private void Update()
    {
        _moveDirection = move.ReadValue<Vector2>();
        _rotateDirection = rotate.ReadValue<Vector2>();
        _currentVelocity = rb.velocity;
        Debug.Log(_rotateDirection);
    }

    private void FixedUpdate()
    {
        MoveForce();
        //Dampening();
        //rb.velocity = new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed);
        rb.AddForce(_force);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        Debug.Log("DASH");
        Vector2 dash = new Vector2(_moveDirection.x * _dashForce, _moveDirection.y * _dashForce);
        Debug.Log(dash);
        rb.velocity = dash;
    }

    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
        dash.Disable();
    }
}
