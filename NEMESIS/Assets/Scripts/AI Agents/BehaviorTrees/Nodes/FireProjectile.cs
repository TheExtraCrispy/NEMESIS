using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : ActionNode
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float refireDelay;
    private float timeToFire = 0;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(Time.time >= timeToFire)
        {
            Instantiate(projectilePrefab, agent.hostTransform.position, agent.hostTransform.rotation);
            timeToFire = Time.time + refireDelay;
            return (State.SUCCESS);
        }
        return State.RUNNING;
    }
}
