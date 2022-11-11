using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : ActionNode
{
    [SerializeField] Vector2 target;
    

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
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
            return (State.FAILURE);
        }
    }
}
