using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyBlueprint : ScriptableObject
{
    public GameObject prefab;
    public AgentStats stats;
    public int pointCost;
}
