using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRangeOfTarget : ActionNode
{

    [SerializeField] float distanceFromTarget;


    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.targetObject != null)
        {
            Vector2 vectorToTarget = ((Vector2)blackboard.targetObject.transform.position - agent.hostRb.position);
            Vector2 directionToTarget = vectorToTarget.normalized;
            float distanceToTarget = vectorToTarget.magnitude;
            float difference = distanceToTarget - distanceFromTarget;
            Vector2 target = agent.hostRb.position + (directionToTarget * difference);
            blackboard.desiredPosition = target;
            if (agent.navAgent.SetDestination(blackboard.desiredPosition))
            {
                return (State.SUCCESS);
            }
            else
            {
                if (agent.debugMode)
                {
                    Debug.Log("Target is not reachable");
                }
            }
        }
        return (State.FAILURE);
    }
}
