using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : DecoratorNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        switch (child.Update())
        {
            case State.RUNNING:
                return (State.RUNNING);
            case State.FAILURE:
                return (State.SUCCESS);
            case State.SUCCESS:
                return (State.FAILURE);
            default:
                started = false;
                return (State.RUNNING);
        }
    }
}
