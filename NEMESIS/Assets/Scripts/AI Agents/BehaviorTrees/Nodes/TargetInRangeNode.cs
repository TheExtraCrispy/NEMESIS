using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetInRangeNode : ActionNode
{
    public float radius;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        float distance = ((Vector2)blackboard.targetTransform.position - agent.hostRb.position).magnitude;
        if (distance <= radius)
        {
            return (State.SUCCESS);
        }
        return (State.FAILURE);
    }
}
