using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMultiplicationNode : ActionNode
{
    public float scalar;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(blackboard.desiredVelocity != null)
        {
            blackboard.desiredVelocity *= scalar;
            return (State.SUCCESS);
        }
        return (State.FAILURE);
    }
}

