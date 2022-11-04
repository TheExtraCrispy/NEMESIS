using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceNode : ActionNode
{
    public float force;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector2 direction = agent.hostTransform.up;
        agent.hostRb.AddForce(direction * force * Time.deltaTime, ForceMode2D.Impulse);
        return (State.SUCCESS);
    }
}
