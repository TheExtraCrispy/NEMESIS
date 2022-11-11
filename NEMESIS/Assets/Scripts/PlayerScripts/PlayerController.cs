using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using System;
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerControls _playerControls;
    private Rigidbody2D rb;
    [SerializeField] Camera _camera;
    private WeaponHandler weaponHandler;
    private PlayerEnergy playerEnergy;
    [SerializeField] Transform _crosshairInner;
    [SerializeField] Transform _crosshairOuter;

    [Header("Movement Settings")]
    [SerializeField] float _acceleration;
    [SerializeField] float _maxMoveSpeed;
    [SerializeField] float _dashForce;
    [SerializeField] float _dashEnergyCost;
    [Space(10)]

    [Header("Aim Settings")]
    [SerializeField] float _aimSpeed;

    
    private Vector2 _currentVelocity;
    private Vector2 _moveDirection;
    private Vector2 _force;

    private Vector2 _aimDirection;
    private Vector2 _targetPosition;

    private string _scheme;

    private InputAction move;
    private InputAction dash;
    private InputAction aim;
    private InputAction primaryFire;
    private InputAction alternateFire;
    private InputAction slot1;
    private InputAction slot2;
    private InputAction slot3;
    private InputAction slot4;

    private bool isDead = false;

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
        //Crosshair positioning
        float distance = transform.InverseTransformPoint(point).magnitude;
        Vector3 outerPosition = new Vector3(point.x, point.y, _crosshairOuter.position.z);
        _crosshairOuter.position = outerPosition;
        _crosshairInner.position = transform.TransformPoint(new Vector2(0, distance));
        Debug.DrawLine(transform.position, _crosshairInner.position, color:Color.red);
        //Rigidbody Rotation
        Vector2 aimDirection = point - rb.position;
        float targetAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg-90;
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetAngle, Time.deltaTime*_aimSpeed));
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
        bool hasEnergy = playerEnergy.UseEnergy(_dashEnergyCost);
        if (hasEnergy)
        {
            Vector2 dash = new Vector2(_moveDirection.x * _dashForce, _moveDirection.y * _dashForce);
            rb.velocity = dash;
        }
    }

    void PrimaryFire(InputAction.CallbackContext context)
    {
        weaponHandler.PrimaryFire(context);
    }
    void AlternateFire(InputAction.CallbackContext context)
    {
        weaponHandler.AlternateFire(context);
    }

    void SelectedSlot1(InputAction.CallbackContext context)
    {
        weaponHandler.ChooseWeapon(1);
    }
    void SelectedSlot2(InputAction.CallbackContext context)
    {
        weaponHandler.ChooseWeapon(2);
    }
    void SelectedSlot3(InputAction.CallbackContext context)
    {
        weaponHandler.ChooseWeapon(3);
    }
    void SelectedSlot4(InputAction.CallbackContext context)
    {
        weaponHandler.ChooseWeapon(4);
    }


    private void MoveForce()
    {
        _force = ((rb.velocity.magnitude <= _maxMoveSpeed) ? _moveDirection * _acceleration : _moveDirection * 0);
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();

        playerEnergy = GetComponent<PlayerEnergy>();
        weaponHandler = GetComponent<WeaponHandler>();

        PlayerEvents.PlayerKilled += OnDeath;
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

        primaryFire = _playerControls.Player.PrimaryFire;
        primaryFire.Enable();
        primaryFire.started += PrimaryFire;
        primaryFire.performed += PrimaryFire;
        primaryFire.canceled += PrimaryFire;

        alternateFire = _playerControls.Player.AltFire;
        alternateFire.Enable();
        alternateFire.started += AlternateFire;
        alternateFire.performed += AlternateFire;
        alternateFire.canceled += AlternateFire;

        slot1 = _playerControls.Player.Weapon1;
        slot1.Enable();
        slot1.performed += SelectedSlot1;

        slot2 = _playerControls.Player.Weapon2;
        slot2.Enable();
        slot2.performed += SelectedSlot2;

        slot3 = _playerControls.Player.Weapon3;
        slot3.Enable();
        slot3.performed += SelectedSlot3;

        slot4 = _playerControls.Player.Weapon4;
        slot4.Enable();
        slot4.performed += SelectedSlot4;
    }

    private void OnDeath(object sender, EventArgs args)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        UnbindControls();
        rb.drag = 0.05f;
        rb.angularDrag = 0.1f;
        primaryFire.Enable();
        primaryFire.started += Restart;
        //this.enabled = false;
    }

    private void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("SampleScene");
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
        

    }

    private void FixedUpdate()
    {
        rb.AddForce(_force);
        RotateToWorldPoint(_targetPosition);
    }

    private void UnbindControls()
    {
        move.Disable();
        dash.Disable();
        aim.Disable();
        primaryFire.Disable();
        alternateFire.Disable();
        slot1.Disable();
        slot2.Disable();
        slot3.Disable();
        slot4.Disable();
    }

    private void OnDisable()
    {
        InputUser.onChange -= OnInputDeviceChange;
        PlayerEvents.PlayerKilled -= OnDeath;
        primaryFire.started -= Restart;
        primaryFire.Disable();
        UnbindControls();
    }
}
