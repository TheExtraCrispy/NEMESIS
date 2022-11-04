using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownNode : ActionNode
{
    public float duration = 1;
    float startTime;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
        startTime = Time.time;
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
            return State.SUCCESS;
        }
        return State.RUNNING;
    }

}
