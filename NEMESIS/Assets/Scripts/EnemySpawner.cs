using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void CreateFromBlueprint(EnemyBlueprint blueprint)
    {
        GameObject prefab = blueprint.prefab;
        Vector3 variance = Random.insideUnitCircle*2;
        variance.z = 0;
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0.5f);
        spawnPosition += variance;
        GameObject spawnedEnemy = Instantiate(prefab, spawnPosition, transform.rotation);
        Agent enemyAgent = spawnedEnemy.GetComponent<Agent>();
        enemyAgent.SetStats(blueprint.stats);
    }
}
