using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAngleRangeNode : ActionNode
{
    public float angleDegrees;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector2 directionToTarget = (agent.hostRb.position - (Vector2)blackboard.targetTransform.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90;
        float angle = (targetAngle-agent.hostRb.rotation) % 360;
        if (Mathf.Abs((Mathf.Abs(angle)-180)) <= angleDegrees)
        {
            return (State.SUCCESS);
        }
        return (State.FAILURE);
        
    }
}