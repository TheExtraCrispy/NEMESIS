using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AgentStats : ScriptableObject
{
    public float health;
    public float score;
    public AgentStats Clone()
    {
        AgentStats stats = Instantiate(this);
        return stats;
    }
}
