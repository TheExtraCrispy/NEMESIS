using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Chaingun : Weapon
{
    private float heat;
    [SerializeField] GameObject bullet;
    private bool leftOrRight;
    [SerializeField] float heatPerShot;
    [SerializeField] float heatMax;
    [SerializeField] float heatDispersion;
    [SerializeField] float heatDispersionRecovery;
    [SerializeField] float heatSlowdown;
    public float realHeatDispersion;

    private Image heatBar;
    private void Fire()
    {

        if (Time.time >= timeLastFired)
        {
            Transform firePoint;
            if (leftOrRight)
            {
                firePoint = leftFirePoint.transform;
            }
            else
            {
                firePoint = rightFirePoint.transform;
            }

            GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
            heat += heatPerShot;
            leftOrRight = !leftOrRight;
            timeLastFired = Time.time + Mathf.Clamp(refireDelay*(heatSlowdown * (heat / heatMax)), refireDelay, 999);
            realHeatDispersion = 0;
        }
    }

    public override void Start()
    {
        heatBar = weaponUI.transform.Find("HeatBackground").transform.Find("HeatBar").GetComponent<Image>();
        timeLastFired = 0;
        primaryIsHeld = false;
        alternateIsHeld = false;
    }
    public override void PrimaryFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Fire();
        }
        if (context.performed)
        {
            primaryIsHeld = true;
        }
        if (context.canceled)
        {
            primaryIsHeld = false;
        }
    }

    public override void AlternateFire(InputAction.CallbackContext context)
    {

    }

    public override void Update()
    {
        if (primaryIsHeld == true)
        {
            Fire();
        }

        realHeatDispersion = Mathf.Clamp(realHeatDispersion + heatDispersionRecovery * Time.deltaTime, 0, heatDispersion);
        heat = Mathf.Clamp(heat - (realHeatDispersion * Time.deltaTime), 0, heatMax);
        heatBar.fillAmount = (heat / heatMax);
    }
}
