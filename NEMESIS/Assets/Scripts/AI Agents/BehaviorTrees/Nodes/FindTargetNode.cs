using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindTargetNode : ActionNode
{
    public float searchRadius;
    public string[] targetTags;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(blackboard.targetTransform == null)
        {
            List<RaycastHit2D> targetHits = new List<RaycastHit2D>();
            RaycastHit2D[] hits = Physics2D.CircleCastAll(agent.hostTransform.position, searchRadius, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (targetTags.Contains(hit.transform.tag))
                {
                    targetHits.Add(hit);
                }
            }
            if (targetHits.Count != 0)
            {
                blackboard.targetTransform = targetHits[0].transform;
                return (State.SUCCESS);
            }
            else
            {
                return (State.FAILURE);
            }
        }
        else
        {
            return (State.SUCCESS);
        }
    }
}
