using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerDamagedArgs : EventArgs
{
    public float damageTaken;

    public PlayerDamagedArgs(float damage)
    {
        damageTaken = damage;
    }
}

public static class PlayerEvents
{
    public static event EventHandler<PlayerDamagedArgs> PlayerDamaged;
    public static event EventHandler PlayerKilled;

    public static void InvokePlayerDamaged(float damage)
    {
        PlayerDamaged(null, new PlayerDamagedArgs(damage));
    }

    public static void InvokePlayerKilled()
    {
        PlayerKilled(null, EventArgs.Empty);
    }
}
