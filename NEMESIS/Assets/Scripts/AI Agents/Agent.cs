using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public GameObject hostObject;
    [HideInInspector] public Transform hostTransform;
    [HideInInspector] public Rigidbody2D hostRb;
    public BehaviorTree behaviorTree;
    public AgentStats stats;
    public Health agentHealth;
    public bool isAsleep;


    private void OnDeath()
    {
        ScoreEvents.InvokeEnemyDeath(stats.score, "killed lol", Time.time);
        Destroy(gameObject);
    }

    public void SetStats(AgentStats newStats)
    {
        stats = newStats.Clone();
        agentHealth.InitializeHealth(stats.health);
    }

    private void Awake()
    {
        stats = stats.Clone();
        agentHealth = gameObject.GetComponent<Health>();
        agentHealth.InitializeHealth(stats.health);
    }

    void Start()
    {
        hostObject = gameObject;
        hostTransform = hostObject.transform;
        hostRb = hostObject.GetComponent<Rigidbody2D>();

        behaviorTree = behaviorTree.Clone();

        behaviorTree.Bind(this);
    }

    // Update is called once per frame
    void Update()
    {
        behaviorTree.Update();

        if(agentHealth.health <= 0)
        {
            OnDeath();
        }
    }
}
