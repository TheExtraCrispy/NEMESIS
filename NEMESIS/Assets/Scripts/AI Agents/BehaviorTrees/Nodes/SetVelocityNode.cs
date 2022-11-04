using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocityNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        agent.hostRb.velocity = blackboard.desiredVelocity;
        return (State.SUCCESS);
    }
}
