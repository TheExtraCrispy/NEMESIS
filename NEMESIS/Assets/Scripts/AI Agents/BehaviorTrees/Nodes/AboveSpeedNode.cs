using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboveSpeedNode : ActionNode
{
    public float maxSpeed;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        float speed = agent.hostRb.velocity.magnitude;
        if (speed > maxSpeed)
        {
            return (State.SUCCESS);
        }
        return (State.FAILURE);
    }
}
