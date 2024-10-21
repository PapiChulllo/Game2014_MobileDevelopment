using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; 
    [SerializeField]
    private float spawnInterval = 3f; // Time between each spawn
    [SerializeField]
    private Vector2 spawnRangeX = new Vector2(-5f, 5f); // X-axis range for random spawn positions

    private bool isSpawning = false; // Ensure spawning only happens once

    void Start()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemies());
            isSpawning = true; // Prevent double initialization
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), 10f, 0f); 
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
