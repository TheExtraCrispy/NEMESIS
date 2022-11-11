using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightToTarget : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector2 directionToTarget =  (blackboard.targetObject.transform.position - agent.hostTransform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(agent.hostTransform.position, directionToTarget, 500f);        
        if (hit.transform == blackboard.targetObject.transform)
        {
            if (agent.debugMode)
            {
                Debug.DrawLine(agent.hostTransform.position, hit.point, Color.green);
            }
            return (State.SUCCESS);
        }
        else
        {
            if (agent.debugMode)
            {
                Debug.DrawLine(agent.hostTransform.position, hit.point, Color.red);
            }
            return (State.FAILURE);
        }
    }
}
