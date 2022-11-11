using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] GameObject _targetObject;
    [SerializeField] float _smoothTime;
    [SerializeField] float _zPosition;
    private Transform _targetTransform;
    private Vector2 _targetPosition;
    private Vector3 _velocity;
    private void Awake()
    {
        _targetTransform = _targetObject.transform;
    }

    private void Start()
    {
        _targetPosition = _targetTransform.position;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        _targetPosition = _targetTransform.position;
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, _zPosition);
    }
}
