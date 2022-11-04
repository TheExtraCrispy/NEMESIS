using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToAngleNode : ActionNode
{
    public float _rotationSpeed;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector2 aimDirection = (Vector2)blackboard.targetTransform.position - agent.hostRb.position;
        float targetAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        agent.hostRb.MoveRotation(Mathf.LerpAngle(agent.transform.rotation.eulerAngles.z, targetAngle, Time.deltaTime * _rotationSpeed));
        return (State.SUCCESS);
    }
}

