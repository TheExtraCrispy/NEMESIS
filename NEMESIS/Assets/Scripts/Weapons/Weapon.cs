using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : ScriptableObject
{
    public GameObject leftFirePoint;
    public GameObject rightFirePoint;
    public GameObject spinalFirePoint;

    public GameObject weaponUI;
    [Header("Weapon Effects and Visuals")]
    public AudioClip[] fireSFX;
    [Space(10)]

    [Header("Weapon Stats:")]
    public float refireDelay;
    public float timeLastFired;
    public bool primaryIsHeld;
    public bool alternateIsHeld;

    public int projectilePoolSize;
    public abstract void PrimaryFire(InputAction.CallbackContext context);
    public abstract void AlternateFire(InputAction.CallbackContext context);

    public abstract void Start();
    public abstract void Update();
}
