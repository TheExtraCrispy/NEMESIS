using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(blackboard.targetObject != null)
        {
            Debug.Log("Has target");
            blackboard.desiredPosition = blackboard.targetObject.transform.position;
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
