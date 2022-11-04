using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DeathContext : EventArgs
{
    public float rawScore;
    public string causeOfDeath;
    public float timeOfDeath;
    public DeathContext(float score, string cause, float time) 
    {
        rawScore = score;
        causeOfDeath = cause;
        timeOfDeath = time;
    }
}

public static class ScoreEvents
{
    public static event EventHandler<DeathContext> EnemyDeath;
    public static event EventHandler WaveComplete;

    public static void InvokeWaveComplete()
    {
        WaveComplete(null, EventArgs.Empty);
    }

    public static void InvokeEnemyDeath(float score, string cause, float time)
    {
        EnemyDeath(null, new DeathContext(score, cause, time));
    }
}
