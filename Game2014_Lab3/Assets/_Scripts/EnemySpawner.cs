using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; // Assign your enemy prefab in the Inspector
    [SerializeField]
    private float spawnInterval = 3f; // Time between each spawn
    [SerializeField]
    private Vector2 spawnRangeX = new Vector2(-5f, 5f); // X-axis range for random spawn positions

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), 10f, 0f); // Adjust spawn position based on your game world
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
