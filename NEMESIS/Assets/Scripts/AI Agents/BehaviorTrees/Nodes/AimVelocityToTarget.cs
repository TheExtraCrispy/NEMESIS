using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimVelocityToTarget : ActionNode
{
    public float magnitude;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector2 direction = ((Vector2)blackboard.targetTransform.position - agent.hostRb.position).normalized;
        blackboard.desiredVelocity = direction * magnitude;

        return (State.SUCCESS);
    }
}
