using System.Collections;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2.0f;
    public float spawnHeightOffset = 2.0f;

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned in the inspector!");
            return;
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
            Debug.Log("Spawning enemy at: " + spawnPosition);

            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, transform.rotation);
            if (spawnedEnemy != null)
            {
                Debug.Log("Successfully spawned enemy: " + spawnedEnemy.name);
            }
            else
            {
                Debug.LogError("Failed to spawn enemy.");
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
