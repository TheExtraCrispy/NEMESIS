using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System;
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerControls _playerControls;
    private Rigidbody2D rb;
    [SerializeField] Camera _camera;
    [Header("Movement Settings")]
    [SerializeField] float _acceleration;
    [SerializeField] float _maxMoveSpeed;
    [SerializeField] float _dashForce;
    [Space(10)]

    [Header("Aim Settings")]

    
    private Vector2 _currentVelocity;
    private Vector2 _moveDirection;
    private Vector2 _force;

    private Vector2 _aimDirection;
    private Vector2 _targetPosition;

    private string _scheme;

    private InputAction move;
    private InputAction dash;
    private InputAction aim;

    private InputAction fire;
    
    Vector2 ControllerAim(Vector2 aimVector)
    {
        Vector2 targetPosition;

        if (Mathf.Approximately(aimVector.magnitude, 0f))
        {
            aimVector = _aimDirection;
        }
        else
        {
            _aimDirection = aimVector;
        }
        targetPosition = (Vector2)transform.position + aimVector;
        return (targetPosition);
    }

    Vector2 MouseAim(Vector2 aimVector)
    {
        Vector2 targetPosition = _camera.ScreenToWorldPoint(aimVector);
        return (targetPosition);
    }

    void RotateToWorldPoint(Vector2 point)
    {
        Vector2 aimDirection = point - rb.position;
        float targetAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg-90;
        rb.rotation = targetAngle;
    }

    void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if(change == InputUserChange.ControlSchemeChanged)
        {
            Debug.Log("CHANGE!");
            _scheme = user.controlScheme.Value.name;
            aim.Reset();
            Debug.Log(_scheme);
        }
    }
    private void Dash(InputAction.CallbackContext context)
    {
        Vector2 dash = new Vector2(_moveDirection.x * _dashForce, _moveDirection.y * _dashForce);
        rb.velocity = dash;
    }

    void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("BANG");
    }
    private void MoveForce()
    {
        _force = ((rb.velocity.magnitude <= _maxMoveSpeed) ? _moveDirection * _acceleration : _moveDirection * 0);
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
        InputUser.onChange += OnInputDeviceChange;

        move = _playerControls.Player.Move;
        move.Enable();

        aim = _playerControls.Player.Aim;
        aim.Enable();

        dash = _playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        fire = _playerControls.Player.PrimaryFire;
        fire.Enable();
        fire.performed += Fire;
    }
    private void Start()
    {
        _scheme = "Keyboard&Mouse";
    }
    private void Update()
    {
        _moveDirection = move.ReadValue<Vector2>();
        _currentVelocity = rb.velocity;
        MoveForce();

        Vector2 aimPosition = aim.ReadValue<Vector2>();

        
        if(_scheme == "Gamepad")
        {
            _targetPosition = ControllerAim(aimPosition);
        }
        if (_scheme == "Keyboard&Mouse")
        {
            _targetPosition = MouseAim(aimPosition);
        }
        RotateToWorldPoint(_targetPosition);

    }

    private void FixedUpdate()
    {
        rb.AddForce(_force);

    }

    private void OnDisable()
    {
        InputUser.onChange -= OnInputDeviceChange;

        move.Disable();
        dash.Disable();
        aim.Disable();
        fire.Disable();
    }
}
