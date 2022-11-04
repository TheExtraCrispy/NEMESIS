using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public void ModifyHealth(float modifier)
    {
        health += modifier;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    public void SetHealth(float modifier)
    {
        health = modifier;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void InitializeHealth(float inputHealth)
    {
        maxHealth = inputHealth;
        health = maxHealth;
    }
}
