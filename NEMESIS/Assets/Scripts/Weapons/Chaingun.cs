using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Chaingun : Weapon
{
    [Header("Primary Fire")]
    private float heat;
    [SerializeField] GameObject bullet;
    private bool leftOrRight;
    [SerializeField] float heatPerShot;
    [SerializeField] float heatMax;
    [SerializeField] float heatDispersion;
    [SerializeField] float heatDispersionRecovery;
    [SerializeField] float heatSlowdown;
    [SerializeField] float primaryRefireDelay;
    public float realHeatDispersion;
    [Space(10)]
    [Header("Alternate Fire")]
    [SerializeField] GameObject heatSinkPrefab;
    [SerializeField] float launchForce;
    [SerializeField] float damageAtMaxHeat;
    [SerializeField] float radiusAtMaxHeat;
    [SerializeField] float forceAtMaxHeat;
    [SerializeField] float alternateRefireDelay;

    private Image heatBar;
    private void Fire()
    {

        if (Time.time >= timePrimaryLastFired)
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
            timePrimaryLastFired = Time.time + Mathf.Clamp(primaryRefireDelay*(heatSlowdown * (heat / heatMax)), primaryRefireDelay, 999);
            realHeatDispersion = 0;
        }
    }

    private void FireHeatsink()
    {
        Debug.Log("ALT FIRE DETECTED");
        if(Time.time >= timeAlternateLastFired)
        {
            Debug.Log("FIRING HEATSINK");
            Transform firePoint = spinalFirePoint.transform;
            Heatsink heatSink = Instantiate(heatSinkPrefab, firePoint.position, firePoint.rotation).GetComponent<Heatsink>();
            float heatFilled = heat / heatMax;
            heatSink.damage = damageAtMaxHeat * heatFilled;
            heatSink.force = forceAtMaxHeat * heatFilled;
            heatSink.radius = radiusAtMaxHeat * heatFilled;
            heatSink.launchForce = launchForce;
            heat = 0;
        }
    }

    public override void Start()
    {
        heatBar = weaponUI.transform.Find("HeatBackground").transform.Find("HeatBar").GetComponent<Image>();
        timePrimaryLastFired = 0;
        timeAlternateLastFired = 0;
        heat = 0;
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
        if (context.started)
        {
            FireHeatsink();
        }
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
