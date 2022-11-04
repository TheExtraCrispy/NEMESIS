using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu()]
public class ProjectileTestWeapon : Weapon
{
    [SerializeField] GameObject projectilePrefab;
    public override void AlternateFire(InputAction.CallbackContext context)
    {
    }

    public override void PrimaryFire(InputAction.CallbackContext context)
    {
        GameObject projectile = Instantiate(projectilePrefab, spinalFirePoint.transform.position, spinalFirePoint.transform.rotation);
    }

    public override void Start()
    {
    }
    public override void Update()
    {
    }
}
