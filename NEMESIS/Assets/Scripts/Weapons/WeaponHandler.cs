using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons;
    [SerializeField] List<GameObject> weaponUIs;
    private Weapon activeWeapon;

    public GameObject leftFirePoint;
    public GameObject rightFirePoint;
    public GameObject spinalFirePoint;

    private void Awake()
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.leftFirePoint = leftFirePoint;
            weapon.rightFirePoint = rightFirePoint;
            weapon.spinalFirePoint = spinalFirePoint;

            weapon.weaponUI = weaponUIs[weapons.IndexOf(weapon)];
            weapon.Start();
        }

        activeWeapon = weapons[0];
    }

    public void PrimaryFire(InputAction.CallbackContext context)
    {
        activeWeapon.PrimaryFire(context);
    }

    public void AlternateFire(InputAction.CallbackContext context)
    {
        activeWeapon.AlternateFire(context);
    }

    public void ChooseWeapon(int slot)
    {
        int targetSlot = slot;
        if (activeWeapon != weapons[targetSlot-1])
        {
            Canvas weaponUICanvas = activeWeapon.weaponUI.GetComponent<Canvas>();
            weaponUICanvas.enabled = false;
            activeWeapon = weapons[targetSlot - 1];
            weaponUICanvas = activeWeapon.weaponUI.GetComponent<Canvas>();
            weaponUICanvas.enabled = true;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        activeWeapon.Update();
    }

    private void OnDisable()
    {
    }
}
