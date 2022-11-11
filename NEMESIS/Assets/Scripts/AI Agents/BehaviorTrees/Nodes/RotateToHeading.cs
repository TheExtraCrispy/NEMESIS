using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotateToHeading : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector2 desiredVelocity = (agent.navAgent.desiredVelocity);
        float desiredAngle = Mathf.Atan2(desiredVelocity.y, desiredVelocity.x) * Mathf.Rad2Deg-90;
        agent.hostRb.MoveRotation(Mathf.LerpAngle(agent.hostRb.rotation, desiredAngle, Time.deltaTime * agent.navAgent.angularSpeed));
        return (State.SUCCESS);
    }
}
