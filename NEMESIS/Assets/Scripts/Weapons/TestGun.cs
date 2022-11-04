using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu()]
public class TestGun : Weapon
{
    [SerializeField] string primaryTapDebugText;
    [SerializeField] string primaryHoldDebugText;
    [SerializeField] string primaryHeldDebugText;
    [SerializeField] string primaryReleaseDebugText;
    [Space(10)]
    [SerializeField] string alternateTapDebugText;
    [SerializeField] string alternatePressDebugText;
    [SerializeField] string alternateHoldDebugText;
    [SerializeField] string alternateHeldDebugText;
    [SerializeField] string alternateReleaseDebugText;


    public override void Start()
    {

    }
    public void Fire(string debugMessage)
    {
        //Debug.Log("Firing, Time is: " + Time.time);
        //Debug.Log("Firing, Target Time is: " + timeLastFired);
        //Debug.Log("Firing, Message is: " + debugMessage);

        if (Time.time >= timeLastFired)
        {
            Debug.Log(debugMessage);
            timeLastFired = Time.time+refireDelay;
        }
    }

    public override void PrimaryFire(InputAction.CallbackContext context)
    {
        //Debug.Log("INTERACTION: " + context.interaction);
        //Debug.Log("STARTED: " + context.started);
        //Debug.Log("PERFORMED: " + context.performed);
        //Debug.Log("CANCELED: " + context.canceled);
        if (context.started)
        {
            Fire(primaryTapDebugText);
        }
        if(context.performed)
        {
            Debug.Log(primaryHoldDebugText);
            primaryIsHeld = true;
        }
        if (context.canceled)
        {
            primaryIsHeld = false;
            Debug.Log(primaryReleaseDebugText);
        }
        
    }

    public override void AlternateFire(InputAction.CallbackContext context)
    {
        if (context.interaction is TapInteraction)
        {
            Fire(alternateTapDebugText);
        }

        if (context.interaction is PressInteraction)
        {
            Fire(alternatePressDebugText);
        }

        if (context.interaction is HoldInteraction)
        {
            alternateIsHeld = true;
            Fire(alternateHoldDebugText);
        }

        if (context.action.WasReleasedThisFrame() && alternateIsHeld)
        {
            alternateIsHeld = false;
            Debug.Log(alternateReleaseDebugText);
        }
    }

    public override void Update()
    {
        if(primaryIsHeld==true)
        {
            Fire(primaryHeldDebugText);
        }
        if (alternateIsHeld==true)
        {
            Fire(alternateHeldDebugText);
        }
    }
}
