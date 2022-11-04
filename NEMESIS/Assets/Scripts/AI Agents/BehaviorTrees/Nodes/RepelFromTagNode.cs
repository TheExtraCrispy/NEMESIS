using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RepelFromTagNode : ActionNode
{
    public float radius;
    public float force;
    public string[] targetTags;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector2 repelDirection = new Vector2();
        repelDirection = Vector2.zero;
        int count = 0;
        Vector2 position = agent.hostRb.position;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, radius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (targetTags.Contains(hit.transform.tag))
            {
                if(hit.rigidbody != agent.hostRb)
                {
                    Vector2 directionToHit = hit.rigidbody.position - position;
                    repelDirection += directionToHit;
                    ++count;
                }
            }
        }
        if (repelDirection != Vector2.zero)
        {
            repelDirection = (repelDirection / count);
            agent.hostRb.AddForce(repelDirection*-force);
            return (State.SUCCESS);
        }
        return (State.FAILURE);
    }
}
