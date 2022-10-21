using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTracking : MonoBehaviour
{
    private GameObject _player;
    private Vector2 _targetMovement;
    [SerializeField] float _deadzone;
    [SerializeField] float _cameraSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerRelativePosition = _player.transform.InverseTransformPoint(transform.position);
        if(Mathf.Abs(playerRelativePosition.magnitude) > _deadzone)
        {
            _targetMovement = playerRelativePosition * (1 - _deadzone / playerRelativePosition.magnitude)*-1;
        }
        else
        {
            _targetMovement = Vector2.zero;
        }        
    }

    private void FixedUpdate()
    {
        transform.Translate(_targetMovement);
    }
}
