using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy prefab to spawn
    public float spawnInterval = 2.0f; // Time interval between spawns
    public float spawnHeightOffset = 2.0f; // Height offset for spawning enemies

    // Start is called before the first frame update
      void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab is not assigned in the inspector.");
            return;
        }
        StartCoroutine(SpawnEnemies());
    }
    // Coroutine to spawn enemies at regular intervals
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Calculate the spawn position above the object
            Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
            // Instantiate the enemy prefab at the calculated position and the object's rotation
            Instantiate(enemyPrefab, spawnPosition, transform.rotation);
            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}