using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public bool debugMode;
    // Start is called before the first frame update
    [HideInInspector] public GameObject hostObject;
    [HideInInspector] public Transform hostTransform;
    [HideInInspector] public Rigidbody2D hostRb;
    [HideInInspector] public NavMeshAgent navAgent;
    public BehaviorTree behaviorTree;
    public AgentStats stats;
    [HideInInspector] public Health agentHealth;
    public bool isAsleep;
    private Vector3 velocity = Vector3.zero;


    private void OnDeath(object sender, DeathEventArgs args)
    {
        ScoreEvents.InvokeEnemyDeath(stats.score, args.causeOfDeath, Time.time);
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

        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
        navAgent.updatePosition = false;

        agentHealth.Killed += OnDeath;
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

        if (debugMode)
        {
            Vector3[] path = navAgent.path.corners;
            for (int i = 0; i < path.Length - 1; i++)
            {
                Debug.DrawLine(path[i], path[i + 1], Color.magenta);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 navDir = ((Vector2)navAgent.nextPosition - hostRb.position);
        if (hostRb.velocity.magnitude <= navAgent.speed)
        {
            hostRb.velocity += navDir * (navAgent.acceleration * Time.deltaTime);
        }
        
        //hostRb.MovePosition(Vector3.SmoothDamp(hostRb.position, navAgent.nextPosition, ref velocity, 0.1f));
    }

    private void OnDisable()
    {
        agentHealth.Killed -= OnDeath;
    }
}
