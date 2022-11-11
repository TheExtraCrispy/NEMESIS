using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(blackboard.targetObject == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                Debug.Log("Setting target to player");
                blackboard.targetObject = player;
                return (State.SUCCESS);
            }
            Debug.Log("No player detected");
            return (State.FAILURE);
        }
        if (agent.debugMode)
        {
            Debug.Log("Has Target Already");
            //Debug.DrawLine(agent.hostTransform.position, blackboard.targetObject.transform.position, Color.cyan);
        }
        return (State.SUCCESS);
    }
}
