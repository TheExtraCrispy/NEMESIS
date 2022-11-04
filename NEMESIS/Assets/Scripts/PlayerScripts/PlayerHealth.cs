using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] float playerMaxHealth;
    [SerializeField] Image healthBar;

    private void Awake()
    {
        InitializeHealth(playerMaxHealth);
    }

    private void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            PlayerEvents.InvokePlayerKilled();
        }
    }
}