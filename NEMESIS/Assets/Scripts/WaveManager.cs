using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemySpawner[] spawners;
    [SerializeField] int startingPoints;
    [SerializeField] List<EnemyBlueprint> blueprints = new List<EnemyBlueprint>();
    [SerializeField] List<EnemyBlueprint> availableBlueprints = new List<EnemyBlueprint>();

    [SerializeField] float waveDelay;

    [SerializeField] private int enemiesRemaining = 0;
    public int availablePoints;
    public int lowestCost;
    int waveNumber = 1;

    private int GetLowestPointCost()
    {
        int lowestCost = -1;
        foreach(EnemyBlueprint blueprint in availableBlueprints)
        {
            if(lowestCost < 0)
            {
                lowestCost = blueprint.pointCost;
            }
            if(blueprint.pointCost < lowestCost)
            {
                lowestCost = blueprint.pointCost;
            }
        }
        return lowestCost;
    }

    /*
    private EnemyBlueprint ChooseRandomBlueprint()
    {
        EnemyBlueprint blueprint;
        while(availableBlueprints.Count>0)
        {
            int index = Random.Range(0, (availableBlueprints.Count));
            blueprint = availableBlueprints[index];
            if (blueprint.pointCost >= availablePoints)
            {
                blueprints.CopyTo(blueprints);
                return blueprint;
            }
            else
            {
                availableBlueprints.Remove(blueprint);
            }
        }
        Debug.Log("No points left for wave manager");
        return null;
    }
    */

    private EnemyBlueprint ChooseRandomBlueprint()
    {
        EnemyBlueprint blueprint;
        int index = Random.Range(0, (availableBlueprints.Count));
        blueprint = availableBlueprints[index];
        return (blueprint);
    }

    private EnemySpawner ChooseRandomSpawner()
    {
        int index = Random.Range(0, (spawners.Length));
        return (spawners[index]);
    }

    private void OnEnemyDeath(object sender, DeathContext context)
    {
        --enemiesRemaining;
        availablePoints += Mathf.CeilToInt(context.rawScore);
        if (enemiesRemaining <= 0)
        {
            Invoke("SpendPoints", waveDelay);
            ++waveNumber;
            ScoreEvents.InvokeWaveComplete();
            Debug.Log("Wave Beaten. Starting Wave " + waveNumber);
        }
    }

    public void SpendPoints()
    {
        while (availablePoints >= lowestCost)
        {
            EnemyBlueprint blueprintToSpawn = ChooseRandomBlueprint();
            EnemySpawner spawnerChosen = ChooseRandomSpawner();

            spawnerChosen.CreateFromBlueprint(blueprintToSpawn);
            availablePoints = Mathf.Max(availablePoints - blueprintToSpawn.pointCost, 0);
            ++enemiesRemaining;
        }
    }

    private void Awake()
    {
        ScoreEvents.EnemyDeath += OnEnemyDeath;
    }

    void Start()
    {
        availablePoints = startingPoints;
        availableBlueprints = blueprints;
        lowestCost = GetLowestPointCost();
        Invoke("SpendPoints", waveDelay);
        Debug.Log("Beginning Wave 1");
    }

    private void OnDisable()
    {
        ScoreEvents.EnemyDeath -= OnEnemyDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
