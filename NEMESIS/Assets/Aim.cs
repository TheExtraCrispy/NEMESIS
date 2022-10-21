using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float _aimspeed;
    [SerializeField] [Range(0, 100)] float _aimDeadzonePercent;


    [SerializeField] int[] _aimAssistRaycastOrder;
    [SerializeField] float _aimAssistWidth;
    [SerializeField] float _aimAssistRange;
    [SerializeField] float _aimAssistStrength;

    private InputAction aim;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    /*
    private Transform CheckForTargets(Vector2 currentPosition, Vector2 direction)
    {
        if (_aimAssistWidth == 0)
        {
            return null;
        }

        int rayCount = _aimAssistRaycastOrder.Length;
        Transform target;

        foreach(int i in _aimAssistRaycastOrder)
        {

        }

    }
    */

    /*
    
    */

    private void Update()
    {
        
    }
}
