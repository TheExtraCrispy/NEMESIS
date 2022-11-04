using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNode : ActionNode
{
    [SerializeField] Node.State setState;
    [SerializeField] bool setStarted;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        started = setStarted;
        return (setState);
    }
}
