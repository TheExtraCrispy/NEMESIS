using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathEventArgs : EventArgs
{
    public string causeOfDeath;
    public DeathEventArgs(string cause)
    {
        causeOfDeath = cause;
    }
}

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public event EventHandler DamageTaken;
    public event EventHandler<DeathEventArgs> Killed;

    public void DamageHealth(float damage, string cause)
    {
        string causeOfDeath = cause;
        //DamageTaken(null, EventArgs.Empty);
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            Killed(null, new DeathEventArgs(causeOfDeath));
        }
    }

    public void ModifyHealth(float modifier)
    {
        if(modifier < 0)
        {
            DamageTaken(null, EventArgs.Empty);
        }
        health += modifier;
        health = Mathf.Clamp(health, 0, maxHealth);
        if(health <= 0)
        {
            Killed(null, new DeathEventArgs("Direct Modification"));
        }
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
